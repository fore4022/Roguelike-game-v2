using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
public class Title_Scene : MonoBehaviour
{
    [SerializeField]
    private EnterMainScene enterMainScene;
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private AudioSource audioSource;

    public string _gameDataPath = "GameData";

    private void Start()
    {
        Managers.Main.GameData.SO = Util.LoadingToPath<GameData_SO>(_gameDataPath, false);
        Managers.Audio.Mixer = audioMixer;

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
        Managers.UI.Get<StartMessage_UI>().SetState();

        yield return new WaitUntil(() => Managers.UserData.data != null);

        Managers.Audio.Init();
        Managers.Audio.InitializedAudio();
        Managers.UI.Get<StartMessage_UI>().SetState();
        audioSource.Play();

        enterMainScene.isLoad = true;
    }
}