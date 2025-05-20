using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class SceneLoading_UI : UserInterface
{
    public const float limitTime = 0.5f;

    private Image background;

    private const float minAlpha = 0;
    private const float maxAlpha = 255;

    private bool wait = true;

    public bool Wait { set { wait = value; } }
    public override void SetUserInterface()
    {
        background = Util.GetComponentInChildren<Image>(transform);

        transform.SetParent(null, false);
        DontDestroyOnLoad(gameObject);
        StartCoroutine(Loading());
    }
    private IEnumerator Loading()
    {
        yield return new WaitUntil(() => Managers.UI.IsInitalized);

        UIElementUtility.SetImageAlpha(background, maxAlpha, limitTime, false);

        yield return new WaitForSecondsRealtime(limitTime);

        if(!Managers.Scene.IsSceneLoadComplete)
        {
            Util.AddressableResourcesRelease();
            StartCoroutine(Managers.Scene.SceneSetting());
        }

        yield return new WaitUntil(() => Managers.Scene.IsSceneLoadComplete);

        TweenSystemManage.Reset();

        yield return new WaitUntil(() => !wait);

        UIElementUtility.SetImageAlpha(background, minAlpha, limitTime);

        yield return new WaitForSecondsRealtime(limitTime);

        Managers.UI.DestroyUI<SceneLoading_UI>();
    }
}