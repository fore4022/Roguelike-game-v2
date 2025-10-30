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
    public Action onLoad = null;
    public Action loadComplete = null;

    private string sceneName = "Title";
    private bool hasInitialization;

    public string CurrentSceneName { get { return sceneName; } }
    // �̺�Ʈ ȣ�� �� ���� ���� UI ���� ����
    public void LoadScene(SceneNames sceneName, bool hasInitialization = true)
    {
        this.sceneName = sceneName.ToString();
        this.hasInitialization = hasInitialization;

        onLoad?.Invoke();

        Managers.UI.Clear();
        Managers.UI.Show<LoadingOverlay_UI>();
        CoroutineHelper.Start(Managers.Scene.SceneSetting());
    }
    // �ʱ�ȭ �۾��� �ִ� �������� ���� �� �ε带 �Ϸ�
    public void LoadComplete()
    {
        loadComplete?.Invoke();

        Managers.UI.Get<LoadingOverlay_UI>().FadeOut();
    }
    // �̺�Ʈ ȣ�� �� ���ο� �� �ε� ���, Fade Out
    public IEnumerator SceneSetting()
    {
        yield return new WaitUntil(() => Managers.UI.Get<LoadingOverlay_UI>() != null);

        yield return new WaitUntil(() => !Managers.UI.Get<LoadingOverlay_UI>().IsFadeIn);

        AddressableHelper.LoadingScene(sceneName.ToString());

        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == sceneName);

        if(!hasInitialization)
        {
            LoadComplete();
        }
    }
}