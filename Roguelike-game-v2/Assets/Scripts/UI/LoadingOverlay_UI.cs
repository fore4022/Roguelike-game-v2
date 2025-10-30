using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class LoadingOverlay_UI : UserInterface
{
    private const float limitTime = 0.5f;

    private Image background;

    private const float minAlpha = 0;
    private const float maxAlpha = 255;

    private bool isFadeIn = true;
    private bool isFadeOut = false;

    public bool IsFadeIn { get { return isFadeIn; } }
    public override void SetUserInterface()
    {
        background = transform.GetComponentInChild<Image>();

        transform.SetParent(null, false);
        DontDestroyOnLoad(gameObject);
        StartCoroutine(Effecting());
    }
    public void FadeOut()
    {
        isFadeOut = true;
    }
    private IEnumerator Effecting()
    {
        yield return new WaitUntil(() => Managers.UI.IsInitalized());

        UIElementUtility.SetImageAlpha(background, maxAlpha, limitTime, false);

        yield return new WaitForSecondsRealtime(limitTime);

        isFadeIn = false;

        yield return new WaitUntil(() => isFadeOut);

        UIElementUtility.SetImageAlpha(background, minAlpha, limitTime);

        yield return new WaitForSecondsRealtime(limitTime);

        Managers.UI.Destroy<LoadingOverlay_UI>();
    }
}