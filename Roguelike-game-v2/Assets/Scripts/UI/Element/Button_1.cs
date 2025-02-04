using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public abstract class Button_1 : UserInterface, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    protected float minScale = 1f;
    protected float maxScale = 1.075f;
    protected float minAlpha = 205f;
    protected float maxAlpha = 255f;
    protected float duration = 0.1f;

    private RectTransform rectTransform;
    private Button button;
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
        if (adjustmentScale != null)
        {
            StopCoroutine(adjustmentScale);
        }

        adjustmentScale = StartCoroutine(Managers.UI.uiElementUtility.SetImageScale(rectTransform, maxScale, duration));
    }
    protected virtual void PointerExit()
    {
        if (isPointerDown) { return; }

        if (adjustmentScale != null)
        {
            StopCoroutine(adjustmentScale);
        }

        adjustmentScale = StartCoroutine(Managers.UI.uiElementUtility.SetImageScale(rectTransform, minScale));
    }
    protected virtual void PointerDown()
    {
        isPointerDown = true;
    }
    protected virtual void PointerUp()
    {
        adjustmentScale = StartCoroutine(Managers.UI.uiElementUtility.SetImageScale(rectTransform, minScale));

        isPointerDown = false;
    }
    public override void SetUserInterface()
    {
        rectTransform = GetComponent<RectTransform>();
        button = GetComponent<Button>();
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

        button.onClick.AddListener(PointerClick);
        Managers.UI.uiElementUtility.SetImageAlpha(image, minAlpha, duration);
    }
    protected virtual void Init() { }
    public abstract void PointerClick();
}