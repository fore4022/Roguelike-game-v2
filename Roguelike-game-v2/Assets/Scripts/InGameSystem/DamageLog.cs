using System.Collections;
using TMPro;
using UnityEngine;
public class DamageLog : MonoBehaviour
{
    private TextMeshProUGUI log;

    private const float defaultFontSize = 36;
    private const float duration = 0.2f;
    private const float targetScale = 0.003f;
    private const float defaultScale = 0.005f;
    private const float targetYPos = 0.075f;

    private void Awake()
    {
        log = GetComponent<TextMeshProUGUI>();  

        log.enabled = false;

        gameObject.SetActive(false);
    }
    public void SetInformation(Vector3 position, float damage)
    {
        if(damage == 0)
        {
            gameObject.SetActive(false);
        }

        float adjustmentFontSize = defaultFontSize;

        transform.position = position;
        log.text = $"{(int)damage:N0}";

        for(int i = 1; i < log.text.Length - ((log.text.Length - 1) % 3); i++)
        {
            adjustmentFontSize /= 2;
        }

        log.fontSize = defaultFontSize + adjustmentFontSize;

        StartCoroutine(Effecting());
    }
    private IEnumerator Effecting()
    {
        Vector3 currentPosition = transform.position;

        UIElementUtility.SetTextAlpha(log, 255);
        transform.SetPosition(currentPosition, currentPosition, duration);

        log.enabled = true;

        yield return new WaitForSeconds(duration);

        log.enabled = false;

        gameObject.SetActive(false);
    }
}