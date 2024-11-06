using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class SceneLoading_UI : UserInterface
{
    public bool isLoading = true;

    private Image image;

    private const float limitTime = 0.2f;
    private const float minAlpha = 0;
    private const float maxAlpha = 255;

    protected override void Awake()
    {
        transform.SetParent(null, false);

        DontDestroyOnLoad(gameObject);

        base.Awake();

        image = GetComponentInChildren<Image>();
    }
    private void Start()
    {
        StartCoroutine(Loading());
    }
    private IEnumerator Loading()
    {
        StartCoroutine(Managers.UI.uiElementUtility.SetImageAlpha(image, maxAlpha, limitTime));

        yield return new WaitForSeconds(limitTime);

        while(isLoading)
        {
            //animation play

            yield return null;
        }

        StartCoroutine(Managers.UI.uiElementUtility.SetImageAlpha(image, minAlpha, limitTime));

        Managers.UI.DestroyUI<SceneLoading_UI>();
    }
}