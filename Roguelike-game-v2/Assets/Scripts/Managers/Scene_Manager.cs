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
    public void LoadScene(SceneName sceneName, bool hasInitialization = true)
    {
        this.sceneName = sceneName.ToString();
        this.hasInitialization = hasInitialization;
        isLoading = true;

        Managers.UI.ClearDictionary();
        Managers.UI.Show<SceneLoading_UI>();

        if(!hasInitialization)
        {
            CoroutineHelper.StartCoroutine(SetSceneLoading());
        }

        loadScene?.Invoke();
    }
    // 이벤트 호출 및 새로운 씬 로드 대기, SceneLoading_UI 활성화
    public IEnumerator SceneSetting()
    {
        if(!isLoading)
        {
            yield break;
        }

        AddressableHelper.LoadingScene(sceneName.ToString());

        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == sceneName);

        loadComplete?.Invoke();

        isLoading = false;

        if(!hasInitialization)
        {
            Managers.UI.Get<SceneLoading_UI>().Wait = false;
        }
    }
    // 씬 로드 이후 추가적인 초기화 과정이 없을 경우, SceneLoading_UI의 대기 상태 해제
    private IEnumerator SetSceneLoading()
    {
        yield return new WaitUntil(() => Managers.UI.Get<SceneLoading_UI>() != null);

        Managers.UI.Get<SceneLoading_UI>().Wait = false;
    }
}