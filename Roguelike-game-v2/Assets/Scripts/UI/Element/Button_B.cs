using UnityEngine;
using UnityEngine.EventSystems;
public abstract class Button_B : Button_Default, IPointerDownHandler, IPointerExitHandler
{
    protected Coroutine adjustmentScale = null;
    protected float minScale = 1;
    protected float maxScale = 1.025f;
    protected float duration = 0.15f;

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        rectTransform.SkipToEnd()
            .SetScale(maxScale, duration);
    }
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.SkipToEnd()
            .SetScale(minScale, duration);
    }
}