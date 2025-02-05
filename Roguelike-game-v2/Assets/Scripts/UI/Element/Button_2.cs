using UnityEngine;
using UnityEngine.EventSystems;
public abstract class Button_2 : Button_Default, IPointerDownHandler, IPointerExitHandler
{
    protected Coroutine adjustmentScale = null;
    protected float minScale = 1;
    protected float maxScale = 1.1f;
    protected float duration = 1;

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (adjustmentScale != null)
        {
            StopCoroutine(adjustmentScale);
        }

        adjustmentScale = StartCoroutine(Managers.UI.uiElementUtility.SetImageScale(rectTransform, maxScale, duration));
    }
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (adjustmentScale != null)
        {
            StopCoroutine(adjustmentScale);
        }

        adjustmentScale = StartCoroutine(Managers.UI.uiElementUtility.SetImageScale(rectTransform, minScale, duration));
    }
}