using UnityEngine;
using UnityEngine.EventSystems;
public class EnterMainScene : MonoBehaviour, IPointerClickHandler
{
    [HideInInspector]
    public bool isLoad = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(isLoad)
        {
            if(TweenSystemManage.IsTweenActive())
            {
                TweenSystemManage.AllSkipToEnd();

                return;
            }

            isLoad = false;

            Managers.UI.GetUI<StartMessage_UI>().SetState();
            Managers.Scene.LoadScene(SceneName.Main, false);
        }
    }
}