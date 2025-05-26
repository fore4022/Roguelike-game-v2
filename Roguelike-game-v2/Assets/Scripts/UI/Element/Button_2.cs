using UnityEngine;
using UnityEngine.EventSystems;
public abstract class Button_2 : Button_Default, IPointerDownHandler, IPointerExitHandler
{
    protected Coroutine adjustmentScale = null;
    protected float minScale = 1;
    protected float maxScale = 1.025f;
    protected float duration = 0.15f;

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        rectTransform.Kill();
        rectTransform.SetScale(maxScale, duration);
    }
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.Kill();
        rectTransform.SetScale(minScale, duration);
    }
}