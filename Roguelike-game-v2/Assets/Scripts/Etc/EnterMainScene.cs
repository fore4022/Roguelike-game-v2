using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// <para>
/// 현재 Title의 Tween 효과가 재생 중이라면, Tween 효과를 종료 시점으로 바꾼다.
/// </para>
/// 현재 Title의 Tween 효과가 재생 중이지 않다면, Main Scene으로 이동한다.
/// </summary>
public class EnterMainScene : MonoBehaviour, IPointerClickHandler
{
    [HideInInspector]
    public bool isLoad = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(isLoad)
        {
            if(Tween_Manage.IsTweenActive())
            {
                Tween_Manage.AllSkipToEnd();

                return;
            }

            isLoad = false;

            Managers.UI.Get<StartMessage_UI>().SetState();
            Managers.Scene.LoadScene(SceneName.Main, false);
        }
    }
}