using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public abstract class Button_2 : MonoBehaviour, IPointerDownHandler, IPointerExitHandler
{
    protected RectTransform rectTransform;
    protected Button button;

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
    public virtual void Set()
    {
        rectTransform = GetComponent<RectTransform>();
        button = GetComponent<Button>();

        button.onClick.AddListener(PointerClick);
    }
    protected abstract void PointerClick();
}