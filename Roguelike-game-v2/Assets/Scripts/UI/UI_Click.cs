using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
public abstract class UI_Click : OnScreenControl, IPointerDownHandler, IPointerClickHandler, IPointerUpHandler
{
    [InputControl(layout = "Button")]
    [SerializeField]
    private string path;

    protected override string controlPathInternal
    {
        get
        {
            return path;
        }
        set
        {
            path = value;
        }
    }
    public abstract void OnPointerDown(PointerEventData eventData);
    public abstract void OnPointerClick(PointerEventData eventData);
    public abstract void OnPointerUp(PointerEventData eventData);
}
