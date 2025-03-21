using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
public class Title_Scene : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameData_SO gameData;

    private bool isLoad = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(isLoad)
        {
            Managers.Scene.LoadScene(Define.SceneName.Main);
        }
    }
    private void Start()
    {
        Managers.Main.GameData.SO = gameData;

        StartCoroutine(Initalizing());
    }
    private IEnumerator Initalizing()
    {
        Managers.UI.InitUI();

        yield return new WaitUntil(() => Managers.UI.IsInitalized);

        Managers.UserData.Load();

        StartCoroutine(UserDataLoading());

        yield return new WaitUntil(() => Managers.UI.GetUI<SceneLoading_UI>() != null);

        Managers.UI.GetUI<SceneLoading_UI>().Wait = false;
    }
    private IEnumerator UserDataLoading()
    {
        Managers.UI.GetUI<StartMessage_UI>().IsLoading(true);

        yield return new WaitUntil(() => Managers.UserData.data != null);

        Managers.UI.GetUI<StartMessage_UI>().IsLoading(false);

        isLoad = true;
    }
}