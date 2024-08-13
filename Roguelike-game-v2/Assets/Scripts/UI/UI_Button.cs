using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
public class UI_Button : OnScreenControl, IPointerEnterHandler, IPointerDownHandler, IPointerClickHandler, IPointerUpHandler, IPointerExitHandler
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
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("enter");
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("down");
    }
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("click");
    }
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("up");
    }
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("exit");
    }
}
