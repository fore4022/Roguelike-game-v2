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
        StartCoroutine(Initalizing());
    }
    private IEnumerator Initalizing()
    {
        Managers.UserData.UserDataLoad();
        Managers.UI.InitUI();

        yield return new WaitUntil(() => Managers.UI.IsInitalize == true);

        StartCoroutine(UserDataLoading());

        yield return new WaitUntil(() => Managers.UI.GetUI<SceneLoading_UI>() != null);

        Managers.UI.GetUI<SceneLoading_UI>().IsLoading = false;
    }
    private IEnumerator UserDataLoading()
    {
        Managers.UI.GetUI<StartMessage_UI>().IsLoading(true);

        yield return new WaitUntil(() => Managers.UserData.GetUserData != null);

        Managers.UI.GetUI<StartMessage_UI>().IsLoading(false);
    }
}