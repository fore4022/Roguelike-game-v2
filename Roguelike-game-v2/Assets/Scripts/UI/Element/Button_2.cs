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
        rectTransform.KillTween();
        rectTransform.SetScale(maxScale, duration);
    }
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.KillTween();
        rectTransform.SetScale(minScale, duration);
    }
}