using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public abstract class Button_Default : MonoBehaviour
{
    protected RectTransform rectTransform;
    protected Button button;

    protected virtual void Awake()
    {
        Init();
    }
    protected virtual void Init()
    {
        rectTransform = GetComponent<RectTransform>();
        button = GetComponent<Button>();

        button.onClick.AddListener(PointerClick);
    }
    protected abstract void PointerClick();
}