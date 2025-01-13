using System.Collections;
using TMPro;
using UnityEngine;
public class StartMessage_UI : MonoBehaviour
{
    private TextMeshProUGUI tmp;

    private const float duration = 1.5f;
    private const int minAlpha = 50;
    private const int maxAlpha = 255;

    private void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        StartCoroutine(Blink());
    }
    private IEnumerator Blink()
    {
        while(true)
        {
            Managers.UI.uiElementUtility.SetTextAlpha(tmp, minAlpha, duration, false);

            yield return new WaitForSecondsRealtime(duration);

            Managers.UI.uiElementUtility.SetTextAlpha(tmp, maxAlpha, duration, false);

            yield return new WaitForSecondsRealtime(duration);
        }
    }
}