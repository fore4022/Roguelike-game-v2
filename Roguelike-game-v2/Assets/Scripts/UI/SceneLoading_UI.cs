using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class SceneLoading_UI : UserInterface
{
    private Image background;

    private const float limitTime = 0.5f;
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

        Managers.Scene.SetScene();

        yield return new WaitUntil(() => Managers.Scene.IsSceneLoadComplete);

        yield return new WaitUntil(() => !wait);

        UIElementUtility.SetImageAlpha(background, minAlpha, limitTime);

        yield return new WaitForSecondsRealtime(limitTime);

        Managers.UI.DestroyUI<SceneLoading_UI>();
    }
}