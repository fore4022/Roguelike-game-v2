using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// <para>
/// 씬 전환과 로딩을 관리한다.
/// </para>
/// 씬 로드와 로딩 완료 이벤트, SceneLoading_UI 제어
/// </summary>
public class Scene_Manager
{
    public Action loadScene = null;
    public Action loadComplete = null;

    private string sceneName = "Title";
    private bool isLoading = false;
    private bool hasInitialization;

    public string CurrentSceneName { get { return sceneName; } }
    public bool IsLoading { get { return !isLoading; } }
    // 이벤트 호출 및 기존 씬의 UI 정보 해제
    public void LoadScene(SceneNames sceneName, bool hasInitialization = true)
    {
        this.sceneName = sceneName.ToString();
        this.hasInitialization = hasInitialization;
        isLoading = true;

        Managers.UI.ClearDictionary();
        Managers.UI.Show<SceneLoading_UI>();
        CoroutineHelper.Start(Managers.Scene.SceneSetting());

        loadScene?.Invoke();
    }
    public void LoadComplete()
    {
        isLoading = false;
    }
    // 이벤트 호출 및 새로운 씬 로드 대기, SceneLoading_UI 활성화
    public IEnumerator SceneSetting()
    {
        if(isLoading)
        {
            loadComplete?.Invoke();

            yield break;
        }

        AddressableHelper.LoadingScene(sceneName.ToString());

        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == sceneName);

        loadComplete?.Invoke();


        if(!hasInitialization)
        {
            isLoading = false;
        }
    }
}