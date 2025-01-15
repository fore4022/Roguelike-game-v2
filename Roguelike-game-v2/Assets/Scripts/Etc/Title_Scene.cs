using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
public class Title_Scene : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Managers.Scene.LoadScene("Main");
    }
    private void Start()
    {
        Managers.UserData.UserDataLoad();
        StartCoroutine(UserDataLoad());
    }
    private IEnumerator UserDataLoad()
    {
        Managers.UI.GetUI<StartMessage_UI>().IsLoading(true);

        yield return new WaitUntil(() => Managers.UserData.GetUserData != null);

        yield return new WaitForSeconds(10);

        Managers.UI.GetUI<StartMessage_UI>().IsLoading(false);
    }
}