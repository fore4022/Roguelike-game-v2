using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
public class UI_Drag : OnScreenStick, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [InputControl(layout = "Image")]
    [SerializeField]
    private string path;

    public void OnBeginDrag(PointerEventData eventData)
    {

    }
    public void OnDrag(PointerEventData eventData)
    {

    }
    public void OnEndDrag(PointerEventData eventData)
    {

    }
}
