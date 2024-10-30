using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Pause_UI : UserInterface, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    private RectTransform rectTransform;
    private Image image;

    private Coroutine adjustmentScale = null;
    private Coroutine adjustmentColor = null;

    private const float minScale = 1f;
    private const float maxScale = 1.035f;
    private const float minAlpha = 155;
    private const float maxAlpha = 235;
    private const float duration = 0.1f;

    private bool isPointerDown = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(adjustmentScale !=  null)
        {
            StopCoroutine(adjustmentScale);
            StopCoroutine(adjustmentColor);
        }

        adjustmentScale = StartCoroutine(Managers.UI.uiElementUtility.SetImageScale(rectTransform, maxScale, duration));
        adjustmentColor = StartCoroutine(Managers.UI.uiElementUtility.SetImageAlpha(image, maxAlpha, duration));
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isPointerDown) { return; }

        if (adjustmentScale != null)
        {
            StopCoroutine(adjustmentScale);
            StopCoroutine(adjustmentColor);
        }

        adjustmentScale = StartCoroutine(Managers.UI.uiElementUtility.SetImageScale(rectTransform, minScale));
        adjustmentColor = StartCoroutine(Managers.UI.uiElementUtility.SetImageAlpha(image, minAlpha));
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;

        Managers.UI.uiElementUtility.SetButtonColor(transform, true);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        adjustmentScale = StartCoroutine(Managers.UI.uiElementUtility.SetImageScale(rectTransform, minScale));
        adjustmentColor = StartCoroutine(Managers.UI.uiElementUtility.SetImageAlpha(image, minAlpha));

        Managers.UI.uiElementUtility.SetButtonColor(transform, false);

        isPointerDown = false;

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Time.timeScale = 0;

        //show UI

        Managers.UI.HideUI<Pause_UI>();
    }
    protected override void Start()
    {
        base.Start();

        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        Set();
    }
    private void Set()
    {
        StartCoroutine(Managers.UI.uiElementUtility.SetImageAlpha(image, minAlpha, duration));
    }
}