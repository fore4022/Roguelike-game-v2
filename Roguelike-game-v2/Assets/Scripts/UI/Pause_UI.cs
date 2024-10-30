using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Pause_UI : UserInterface, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private Icon icon;
    private RectTransform rectTransform;
    private Button button;
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
        adjustmentColor = StartCoroutine(Managers.UI.uiElementUtility.SetImageAlpha(image, maxAlpha, duration));
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (adjustmentScale != null)
        {
            StopCoroutine(adjustmentScale);
            StopCoroutine(adjustmentColor);
        }

        Managers.UI.uiElementUtility.SetButtonState(button, 0);
        icon.UpdateColor();

        adjustmentScale = StartCoroutine(Managers.UI.uiElementUtility.SetImageScale(rectTransform, minScale, duration));
        adjustmentColor = StartCoroutine(Managers.UI.uiElementUtility.SetImageAlpha(image, minAlpha, duration));
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        icon.UpdateColor(button);
    }
    private void PointerClick()
    {
        Time.timeScale = 0;

        //show UI

        Managers.UI.HideUI<Pause_UI>();
    }
    private void OnEnable()
    {
        if(icon == null)
        {
            return;
        }

        icon.UpdateColor();
    }
    protected override void Start()
    {
        base.Start();

        icon = Util.GetComponentInChildren<Icon>(transform);
        rectTransform = GetComponent<RectTransform>();
        button = GetComponent<Button>();
        image = GetComponent<Image>();

        Set();
    }
    private void Set()
    {
        button.onClick.AddListener(PointerClick);

        StartCoroutine(Managers.UI.uiElementUtility.SetImageAlpha(image, minAlpha, duration));
    }
}