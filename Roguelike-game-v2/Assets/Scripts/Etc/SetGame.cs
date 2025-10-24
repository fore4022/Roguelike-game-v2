using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
/// <summary>
/// ���� ���� �ҷ����� ��, �Ҹ� ���� ����
/// </summary>
public class SetGame : MonoBehaviour
{
    [SerializeField]
    private EnterMainScene enterMainScene;
    [SerializeField]
    private AudioSource audioSource;

    private const string _stageDataPath = "StageDatas";

    private void Start()
    {
        StartCoroutine(Initializing());
    }
    private async Task LoadStageDatas()
    {
        Managers.Main.stageDatas.SO = await AddressableHelper.LoadingToPath<StageDatas_SO>(_stageDataPath, false);
    }
    private IEnumerator Initializing()
    {
        Task loadStageDatas = LoadStageDatas();

        yield return new WaitUntil(() => loadStageDatas.IsCompleted);

        Managers.Data.Load();

        yield return new WaitUntil(() => Managers.UI.IsInitalized());

        yield return new WaitUntil(() => Managers.Audio.Mixer != null);

        StartCoroutine(UserDataLoading());
    }
    private IEnumerator UserDataLoading()
    {
        yield return new WaitUntil(() => Managers.Data.user != null);

        Managers.Audio.Init();
        Managers.Audio.InitializedAudio();
        Managers.UI.Get<StartMessage_UI>().SetState();
        audioSource.Play();

        enterMainScene.isLoad = true;
    }
}