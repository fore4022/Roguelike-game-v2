using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
public class Audio_Manager
{
    private AudioMixer _audioMixer;

    private const float _maxValue_BGM = -5;
    private const float _maxValue_FX = 2.5f;
    private const float _minValue = -80;

    public AudioMixer Mixer { get { return _audioMixer; } set { _audioMixer = value; } }
    public void Init()
    {
        SetGroup(SoundTypes.FX, Managers.Data.user.FX);
        SetGroup(SoundTypes.BGM, Managers.Data.user.BGM);
    }
    public void InitializedAudio()
    {
        GameObject[] objs = Object.FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (GameObject obj in objs)
        {
            if(obj.TryGetComponent(out AudioSource audio))
            {
                Registration(audio);
            }
        }
    }
    public void Registration(AudioSource source)
    {
        if(_audioMixer == null)
        {
            CoroutineHelper.StartCoroutine(WaitForAudioMixer(source));
        }
        else
        {
            SetOutputMixerGroup(source);
        }
    }
    public void SetGroup(SoundTypes type)
    {
        if(type == SoundTypes.FX)
        {
            SetGroup(type, Managers.Data.user.SetFX());
        }
        else if(type == SoundTypes.BGM)
        {
            SetGroup(type, Managers.Data.user.SetBGM());
        }
    }
    private void SetGroup(SoundTypes type, bool isActive)
    {
        float value = default;

        if(isActive)
        {
            if(type == SoundTypes.FX)
            {
                value = _maxValue_FX;
            }
            else if(type == SoundTypes.BGM)
            {
                value = _maxValue_BGM;
            }
        }
        else
        {
            value = _minValue;
        }

        _audioMixer.SetFloat(type.ToString(), value);
    }
    public void SetOutputMixerGroup(AudioSource audio)
    {
        if(audio.outputAudioMixerGroup == null)
        {
            audio.outputAudioMixerGroup = _audioMixer.FindMatchingGroups("Master/FX")[0];
        }
        else
        {
            if(audio.outputAudioMixerGroup.name == "FX")
            {
                audio.outputAudioMixerGroup = _audioMixer.FindMatchingGroups("Master/FX")[0];
            }
            else if(audio.outputAudioMixerGroup.name == "BGM")
            {
                audio.outputAudioMixerGroup = _audioMixer.FindMatchingGroups("Master/BGM")[0];
            }
        }

        if(audio.playOnAwake)
        {
            if(audio.gameObject.activeSelf)
            {
                audio.Play();
            }
        }
    }
    private IEnumerator WaitForAudioMixer(AudioSource source)
    {
        source.Stop();

        yield return new WaitUntil(() => _audioMixer != null);

        SetOutputMixerGroup(source);
    }
}