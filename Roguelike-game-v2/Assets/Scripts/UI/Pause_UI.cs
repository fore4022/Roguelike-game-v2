using UnityEngine;
using UnityEngine.EventSystems;
public class Pause_UI : UserInterface, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {

    }
    public void OnPointerExit(PointerEventData eventData)
    {

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Time.timeScale = 0;

        //
    }
}