using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Pause_UI : UserInterface, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(adjustmentScale !=  null)
        {
            StopCoroutine(adjustmentScale);
            StopCoroutine(adjustmentColor);
        }

        adjustmentScale = StartCoroutine(Managers.UI.uiElementUtility.SetImageScale(rectTransform, maxScale, duration));
        adjustmentColor = StartCoroutine(Managers.UI.uiElementUtility.SetImageColor(image, maxAlpha, duration));
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (adjustmentScale != null)
        {
            StopCoroutine(adjustmentScale);
            StopCoroutine(adjustmentColor);
        }

        adjustmentScale = StartCoroutine(Managers.UI.uiElementUtility.SetImageScale(rectTransform, minScale, duration));
        adjustmentColor = StartCoroutine(Managers.UI.uiElementUtility.SetImageColor(image, minAlpha, duration));
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Time.timeScale = 0;

        //show UI
    }
    protected override void Start()
    {
        base.Start();

        rectTransform = Util.GetComponentInChildren<RectTransform>(transform);
        image = Util.GetComponentInChildren<Image>(transform);

        Set();
    }
    private void Set()
    {
        Color color = image.color;

        Debug.Log(color.a);

        color.a = minAlpha / 255;
        
        Debug.Log(color.a);

        image.color = color;
    }
}