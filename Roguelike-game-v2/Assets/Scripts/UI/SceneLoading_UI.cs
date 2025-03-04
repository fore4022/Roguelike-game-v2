using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class SceneLoading_UI : UserInterface
{
    private Image background;

    private const float limitTime = 0.5f;
    private const float minAlpha = 0;
    private const float maxAlpha = 255;

    private bool isLoading = true;

    public bool IsLoading { set { isLoading = value; } }
    public override void SetUserInterface()
    {
        transform.SetParent(null, false);

        DontDestroyOnLoad(gameObject);

        background = Util.GetComponentInChildren<Image>(transform);

        StartCoroutine(Loading());
    }
    private IEnumerator Loading()
    {
        if(!isInitalized)
        {
            Managers.UI.InitUI();

            yield return new WaitUntil(() => Managers.UI.isInitalized);
        }

        Managers.UI.uiElementUtility.SetImageAlpha(background, maxAlpha, limitTime, false);

        yield return new WaitForSecondsRealtime(limitTime);

        Managers.Scene.SetScene();

        yield return new WaitUntil(() => Managers.Scene.IsSceneLoadComplete);

        yield return new WaitUntil(() => isLoading == false);

        Managers.UI.uiElementUtility.SetImageAlpha(background, minAlpha, limitTime);

        yield return new WaitForSecondsRealtime(limitTime);

        Managers.UI.DestroyUI<SceneLoading_UI>();
    }
}