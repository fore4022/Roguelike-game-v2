using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
/// <summary>
/// 유저 정보 불러오기 및, 소리 설정 적용
/// </summary>
public class SetGame : MonoBehaviour
{
    [SerializeField]
    private EnterMainScene enterMainScene;
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private AudioSource audioSource;

    private const string _stageDataPath = "StageDatas";

    private void Start()
    {
        Managers.Audio.Mixer = audioMixer;

        Managers.Scene.loadComplete += Managers.Audio.InitializedAudio;

        StartCoroutine(Initializing());
    }
    private async Task LoadStageDatas()
    {
        Managers.Main.stageDatas.SO = await Util.LoadingToPath<StageDatas_SO>(_stageDataPath, false);
    }
    private IEnumerator Initializing()
    {
        Task loadStageDatas = LoadStageDatas();

        yield return new WaitUntil(() => loadStageDatas.IsCompleted);

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