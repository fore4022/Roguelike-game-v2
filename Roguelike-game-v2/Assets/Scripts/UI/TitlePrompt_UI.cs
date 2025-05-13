using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
public class TitlePrompt_UI : UserInterface, IPointerClickHandler
{
    private const string _gameDataPath = "GameData";

    private bool _isLoad = false;

    public override void SetUserInterface()
    {
        Managers.Main.GameData.SO = Util.LoadingToPath<GameData_SO>(_gameDataPath, false);
        StartCoroutine(Initalizing());
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(_isLoad)
        {
            _isLoad = false;

            Managers.UI.GetUI<StartMessage_UI>().SetState();
            Managers.Scene.LoadScene(SceneName.Main, false);
        }
        else
        {
            TweenSystemManage.AllSkipToEnd();
        }
    }
    public IEnumerator UserDataLoading()
    {
        Managers.UI.ShowAndGet<StartMessage_UI>().SetState();

        yield return new WaitUntil(() => Managers.UserData.data != null);

        Managers.UI.GetUI<StartMessage_UI>().SetState();

        _isLoad = true;
    }
    private IEnumerator Initalizing()
    {
        yield return new WaitUntil(() => Managers.UI.IsInitalized);

        Managers.UserData.Load();
    }
}