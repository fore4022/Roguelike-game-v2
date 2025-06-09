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
        rectTransform.SkipToEnd()
            .SetScale(maxScale, duration);
    }
    protected virtual void PointerExit()
    {
        if(isPointerDown) { return; }

        rectTransform.SkipToEnd()
            .SetScale(minScale, duration);
    }
    protected virtual void PointerDown()
    {
        isPointerDown = true;
    }
    protected virtual void PointerUp()
    {
        rectTransform.SetScale(minScale, 0);

        isPointerDown = false;
    }
    private void Set()
    {
        UIElementUtility.SetImageAlpha(image, maxAlpha, duration);
    }
    protected override void Init()
    {
        image = GetComponent<Image>();

        base.Init();
        Set();
    }
}