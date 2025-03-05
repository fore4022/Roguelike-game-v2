using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public abstract class Button_1 : Button_Default, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    protected float minScale = 1f;
    protected float maxScale = 1.075f;
    protected float minAlpha = 205f;
    protected float maxAlpha = 255f;
    protected float duration = 0.1f;

    private Image image;

    private Coroutine adjustmentScale = null;
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
    protected virtual void PointerEnter()
    {
        if(adjustmentScale != null)
        {
            StopCoroutine(adjustmentScale);
        }

        adjustmentScale = StartCoroutine(UIElementUtility.SetImageScale(rectTransform, maxScale, duration));
    }
    protected virtual void PointerExit()
    {
        if(isPointerDown) { return; }

        if(adjustmentScale != null)
        {
            StopCoroutine(adjustmentScale);
        }

        UIElementUtility.SetImageScale(rectTransform, minScale);
    }
    protected virtual void PointerDown()
    {
        isPointerDown = true;
    }
    protected virtual void PointerUp()
    {
        UIElementUtility.SetImageScale(rectTransform, minScale);

        isPointerDown = false;
    }
    private void Set()
    {
        if(minScale != 1)
        {
            UIElementUtility.SetImageScale(rectTransform, minScale);
        }

        UIElementUtility.SetImageAlpha(image, minAlpha, duration);
    }
    protected override void Init()
    {
        image = GetComponent<Image>();

        base.Init();
        Set();
    }
}