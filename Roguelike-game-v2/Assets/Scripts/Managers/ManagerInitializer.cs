using UnityEngine;
using UnityEngine.Audio;
/// <summary>
/// 매니저 초기화 스크립트
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