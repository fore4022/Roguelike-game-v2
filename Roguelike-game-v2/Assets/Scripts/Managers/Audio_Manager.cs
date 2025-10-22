using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
public class Audio_Manager
{
    private AudioMixer _audioMixer;
    private AudioMixerGroup fx;
    private AudioMixerGroup bgm;

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
        GameObject[] objs = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        foreach(GameObject obj in objs)
        {
            if(obj.TryGetComponent(out AudioSource audio))
            {
                Registration(audio);
            }
        }
    }
    public void Registration(AudioSource source, SoundTypes? type = null)
    {
        if(_audioMixer == null)
        {
            CoroutineHelper.StartCoroutine(WaitForAudioMixer(source, type));
        }
        else
        {
            SetOutputMixerGroup(source, type);
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
    public void SetOutputMixerGroup(AudioSource source, SoundTypes? type = null)
    {
        if(type == SoundTypes.FX)
        {
            source.outputAudioMixerGroup = _audioMixer.FindMatchingGroups("Master/FX")[0];
        }
        else if(type == SoundTypes.BGM)
        {
            source.outputAudioMixerGroup = _audioMixer.FindMatchingGroups("Master/BGM")[0];
        }

        if(source.playOnAwake)
        {
            source.Play();
        }
    }
    private IEnumerator WaitForAudioMixer(AudioSource source, SoundTypes? type = null)
    {
        source.Stop();

        yield return new WaitUntil(() => _audioMixer != null);

        SetOutputMixerGroup(source, type);
    }
}