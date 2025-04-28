using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
public class Title_Scene : MonoBehaviour, IPointerClickHandler
{
    private const string gameDataPath = "GameData";

    private bool isLoad = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(isLoad)
        {
            isLoad = false;

            Managers.UI.GetUI<StartMessage_UI>().SetState();
            Managers.Scene.LoadScene(SceneName.Main, false);
        }
    }
    private void Start()
    {
        Managers.Main.GameData.SO = Util.LoadingToPath<GameData_SO>(gameDataPath, false);

        StartCoroutine(Initalizing());
    }
    private IEnumerator Initalizing()
    {
        yield return new WaitUntil(() => Managers.UI.IsInitalized);

        Managers.UserData.Load();

        StartCoroutine(UserDataLoading());

        yield return new WaitUntil(() => Managers.UI.GetUI<SceneLoading_UI>() != null);

        Managers.UI.GetUI<SceneLoading_UI>().Wait = false;
    }
    private IEnumerator UserDataLoading()
    {
        Managers.UI.GetUI<StartMessage_UI>().SetState();

        yield return new WaitUntil(() => Managers.UserData.data != null);

        Managers.UI.GetUI<StartMessage_UI>().SetState();

        isLoad = true;
    }
}