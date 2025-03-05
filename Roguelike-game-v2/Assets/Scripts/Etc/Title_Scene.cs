using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
public class Title_Scene : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameData_SO gameData;

    public void OnPointerClick(PointerEventData eventData)
    {
        Managers.Scene.LoadScene(Define.SceneName.Main);
    }
    private void Start()
    {
        Managers.Main.GameData.SO = gameData;

        StartCoroutine(Initalizing());
    }
    private IEnumerator Initalizing()
    {
        Managers.UserData.UserDataLoad();
        Managers.UI.InitUI();

        yield return new WaitUntil(() => Managers.UI.isInitalized);

        StartCoroutine(UserDataLoading());

        yield return new WaitUntil(() => Managers.UI.GetUI<SceneLoading_UI>() != null);

        Managers.UI.GetUI<SceneLoading_UI>().IsLoading = false;
    }
    private IEnumerator UserDataLoading()
    {
        Managers.UI.GetUI<StartMessage_UI>().IsLoading(true);

        yield return new WaitUntil(() => Managers.UserData.data != null);

        Managers.UI.GetUI<StartMessage_UI>().IsLoading(false);
    }
}