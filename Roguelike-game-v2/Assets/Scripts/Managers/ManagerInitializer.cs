using UnityEngine;
using UnityEngine.Audio;
/// <summary>
/// �Ŵ��� �ʱ�ȭ ��ũ��Ʈ
/// </summary>
public class ManagerInitializer : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    private void Awake()
    {
        Managers.Audio.Mixer = audioMixer;
    }
}