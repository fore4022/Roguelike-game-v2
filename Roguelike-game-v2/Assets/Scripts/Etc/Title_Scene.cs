using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
/// <summary>
/// ���� ���� �ҷ����� ��, �Ҹ� ���� ����
/// </summary>
public class Title_Scene : MonoBehaviour
{
    [SerializeField]
    private EnterMainScene enterMainScene;
    [SerializeField]
    private Camera_SizeScale cameraSizeScale;
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private AudioSource audioSource;

    public const string _gameDataPath = "StageDatas";

    private async void Start()
    {
        Managers.Main.GameData.SO = await Util.LoadingToPath<StageDatas_SO>(_gameDataPath, false);
        Managers.Audio.Mixer = audioMixer;

        Managers.Scene.loadComplete += Managers.Audio.InitializedAudio;

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
        yield return new WaitUntil(() => Managers.UserData.data != null);

        Managers.Audio.Init();
        Managers.Audio.InitializedAudio();
        Managers.UI.Get<StartMessage_UI>().SetState();
        audioSource.Play();

        enterMainScene.isLoad = true;
    }
}