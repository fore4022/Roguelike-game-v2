using UnityEngine;
using UnityEngine.Audio;
/// <summary>
/// �Ŵ��� �ʱ�ȭ ��ũ��Ʈ
/// </summary>
public class Manager_Initializer : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    private void Awake()
    {
        Managers.Audio.Mixer = audioMixer;
    }
}