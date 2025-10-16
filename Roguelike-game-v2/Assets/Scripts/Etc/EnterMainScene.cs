using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// <para>
/// ���� Title�� Tween ȿ���� ��� ���̶��, Tween ȿ���� ���� �������� �ٲ۴�.
/// </para>
/// ���� Title�� Tween ȿ���� ��� ������ �ʴٸ�, Main Scene���� �̵��Ѵ�.
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