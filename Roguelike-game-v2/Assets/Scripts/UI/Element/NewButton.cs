using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public abstract class NewButton : UserInterface, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    protected float minScale;
    protected float maxScale;
    protected float minAlpha;
    protected float maxAlpha;
    protected float duration;

    private RectTransform rectTransform;
    private Image image;

    private Coroutine adjustmentScale = null;
    private Coroutine adjustmentColor = null;

    private bool isPointerDown = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        PointerEnter();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        PointerExit();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        PointerDown();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        PointerUp();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        PointerClick();
    }
    protected virtual void PointerEnter()
    {
        if (adjustmentScale != null)
        {
            StopCoroutine(adjustmentScale);
            StopCoroutine(adjustmentColor);
        }

        adjustmentScale = StartCoroutine(Managers.UI.uiElementUtility.SetImageScale(rectTransform, maxScale, duration));
        adjustmentColor = StartCoroutine(Managers.UI.uiElementUtility.SetImageAlpha(image, maxAlpha, duration));
    }
    protected virtual void PointerExit()
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
    protected virtual void PointerDown()
    {
        isPointerDown = true;

        Managers.UI.uiElementUtility.SetButtonColor(transform, true);
    }
    protected virtual void PointerUp()
    {
        adjustmentScale = StartCoroutine(Managers.UI.uiElementUtility.SetImageScale(rectTransform, minScale));
        adjustmentColor = StartCoroutine(Managers.UI.uiElementUtility.SetImageAlpha(image, minAlpha));

        Managers.UI.uiElementUtility.SetButtonColor(transform, false);

        isPointerDown = false;
    }
    public override void SetUI()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        Init();

        Set();
    }
    private void Set()
    {
        if(minScale != 1)
        {
            StartCoroutine(Managers.UI.uiElementUtility.SetImageScale(rectTransform, minScale));
        }

        StartCoroutine(Managers.UI.uiElementUtility.SetImageAlpha(image, minAlpha, duration));
    }
    protected abstract void PointerClick();
    protected abstract void Init();
}