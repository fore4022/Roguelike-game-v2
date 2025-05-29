using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
public class Title_Scene : MonoBehaviour
{
    [SerializeField]
    private EnterMainScene enterMainScene;
    [SerializeField]
    private AudioMixer audioMixer;

    private AudioSource audioSource;

    private const string _gameDataPath = "GameData";

    private void Start()
    {
        Managers.Main.GameData.SO = Util.LoadingToPath<GameData_SO>(_gameDataPath, false);
        Managers.Audio.Mixer = audioMixer;
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(Initializing());
    }
    private IEnumerator Initializing()
    {
        Managers.UserData.Load();

        yield return new WaitUntil(() => Managers.UI.IsInitalized);

        Managers.Audio.Init();

        yield return new WaitForEndOfFrame();

        audioSource.Play();
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