using UnityEngine;
using UnityEngine.EventSystems;
public class Test : UI_Button
{
    public override void OnPointerClick(PointerEventData eventData)
    {

    }
    public override void OnPointerDown(PointerEventData eventData)
    {

    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        SendValueToControl(true);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {

    }
    public override void OnPointerUp(PointerEventData eventData)
    {

    }
}