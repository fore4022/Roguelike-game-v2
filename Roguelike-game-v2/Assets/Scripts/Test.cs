using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Test : UI_Click
{
    public override void OnPointerEnter(PointerEventData eventData)
    {
        SendValueToControl<bool>(false);
        Debug.Log("DownPointer");
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("UpPointer");
    }
}