using System.Collections;
using UnityEngine;
public class Title_Scene : MonoBehaviour
{
    [SerializeField]
    private EnterMainScene enterMainScene;

    private const string _gameDataPath = "GameData";

    private void Start()
    {
        Managers.Main.GameData.SO = Util.LoadingToPath<GameData_SO>(_gameDataPath, false);

        StartCoroutine(Initializing());
    }
    private IEnumerator Initializing()
    {
        Managers.UserData.Load();

        yield return new WaitUntil(() => Managers.UI.IsInitalized);

        StartCoroutine(UserDataLoading());
    }
    private IEnumerator UserDataLoading()
    {
        Managers.UI.GetUI<StartMessage_UI>().SetState();

        yield return new WaitUntil(() => Managers.UserData.data != null);

        Managers.UI.GetUI<StartMessage_UI>().SetState();

        enterMainScene.isLoad = true;
    }
}