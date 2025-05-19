using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ToastMessage_UI : UserInterface
{
    private Image img;
    private TextMeshProUGUI text;

    private const float delay = 1.25f;

    private Coroutine coroutine_img = null;
    private Coroutine coroutine_text = null;
    private Coroutine coroutine = null;

    public override void SetUserInterface()
    {
        img = GetComponent<Image>();
        text = Util.GetComponentInChildren<TextMeshProUGUI>(transform);

        Managers.UI.HideUI<ToastMessage_UI>();
    }
    protected override void Enable()
    {
        coroutine = StartCoroutine(ToastHide());
    }
    private void OnDisable()
    {
        if(coroutine != null)
        {
            Util.StopCoroutine(coroutine_img);
            Util.StopCoroutine(coroutine_text);

            coroutine = null;
        }
    }
    private IEnumerator ToastHide()
    {
        UIElementUtility.SetImageAlpha(img, 50);
        UIElementUtility.SetTextAlpha(text, 255);

        yield return new WaitForSeconds(delay);

        coroutine_img = UIElementUtility.SetImageAlpha(img, 0, delay);
        coroutine_text = UIElementUtility.SetTextAlpha(text, 0, delay);

        yield return new WaitForSeconds(delay + 0.5f);

        coroutine = null;

        Managers.UI.HideUI<ToastMessage_UI>();
    }
}