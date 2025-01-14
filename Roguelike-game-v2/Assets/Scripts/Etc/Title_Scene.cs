using UnityEngine;
using UnityEngine.EventSystems;
public class Title_Scene : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Managers.Scene.LoadScene("Main");
    }
}