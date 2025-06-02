using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
public class Title_Scene : MonoBehaviour
{
    [SerializeField]
    private EnterMainScene enterMainScene;
    [SerializeField]
    private AudioMixer audioMixer;

    private const string _gameDataPath = "GameData";

    private void Start()
    {
        Managers.Main.GameData.SO = Util.LoadingToPath<GameData_SO>(_gameDataPath, false);
        Managers.Audio.Mixer = audioMixer;

        StartCoroutine(Initializing());
    }
    private IEnumerator Initializing()
    {
        Managers.UserData.Load();
        Managers.Audio.InitializedAudio();

        yield return new WaitUntil(() => Managers.UI.IsInitalized);

        StartCoroutine(UserDataLoading());
    }
    private IEnumerator UserDataLoading()
    {
        Managers.UI.GetUI<StartMessage_UI>().SetState();

        yield return new WaitUntil(() => Managers.UserData.data != null);

        Managers.Audio.Init();
        Managers.UI.GetUI<StartMessage_UI>().SetState();

        enterMainScene.isLoad = true;
    }
}