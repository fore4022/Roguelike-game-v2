using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// <para>
/// �� ��ȯ�� �ε��� �����Ѵ�.
/// </para>
/// �� �ε�� �ε� �Ϸ� �̺�Ʈ, SceneLoading_UI ����
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
    // �̺�Ʈ ȣ�� �� ���� ���� UI ���� ����
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
    // �̺�Ʈ ȣ�� �� ���ο� �� �ε� ���, SceneLoading_UI Ȱ��ȭ
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