using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
public class TitlePrompt_UI : MonoBehaviour, IPointerClickHandler
{
    private const string _gameDataPath = "GameData";

    private bool _isLoad = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(_isLoad)
        {
            _isLoad = false;

            Managers.UI.GetUI<StartMessage_UI>().SetState();
            Managers.Scene.LoadScene(SceneName.Main, false);
        }
    }
    private void Start()
    {
        Managers.Main.GameData.SO = Util.LoadingToPath<GameData_SO>(_gameDataPath, false);

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

        _isLoad = true;
    }
}