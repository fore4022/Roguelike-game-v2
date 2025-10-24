using UnityEngine;
using UnityEngine.Audio;
public class ManagerInitializer : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    private void Awake()
    {
        Managers.Audio.Mixer = audioMixer;
    }
}