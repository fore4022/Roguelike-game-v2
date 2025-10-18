using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ToastMessage_UI : UserInterface
{
    private Image img;
    private TextMeshProUGUI toast;

    private Coroutine coroutine_img = null;
    private Coroutine coroutine_text = null;
    private Coroutine coroutine = null;

    private const float delay = 1.25f;

    public override void SetUserInterface()
    {
        img = GetComponent<Image>();
        toast = transform.GetComponentInChild<TextMeshProUGUI>();

        Managers.UI.Hide<ToastMessage_UI>();
    }
    protected override void Enable()
    {
        coroutine = StartCoroutine(ToastHide());
    }
    public void SetText(string text)
    {
        toast.text = text;
    }
    private void OnDisable()
    {
        if(coroutine != null)
        {
            if(coroutine_img != null)
            {
                CoroutineHelper.StopCoroutine(coroutine_img);
                CoroutineHelper.StopCoroutine(coroutine_text);
            }

            coroutine = null;
        }
    }
    private IEnumerator ToastHide()
    {
        yield return new WaitUntil(() => toast.text != "");

        UIElementUtility.SetImageAlpha(img, 50);
        UIElementUtility.SetTextAlpha(toast, 255);

        yield return new WaitForSeconds(delay);

        coroutine_img = UIElementUtility.SetImageAlpha(img, 0, delay);
        coroutine_text = UIElementUtility.SetTextAlpha(toast, 0, delay);

        yield return new WaitForSeconds(delay + 0.5f);

        coroutine = null;
        toast.text = "";

        Managers.UI.Hide<ToastMessage_UI>();
    }
}