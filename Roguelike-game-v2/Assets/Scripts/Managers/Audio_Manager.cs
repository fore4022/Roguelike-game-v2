using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class Audio_Manager
{
    private List<AudioSource> _sources = new();
    private AudioMixer _audioMixer;

    private const SoundTypes _bgm = SoundTypes.BGM;
    private const SoundTypes _fx = SoundTypes.FX;
    private const float _maxValue_BGM = -5;
    private const float _maxValue_FX = 2.5f;
    private const float _minValue = -80;
    
    public AudioMixer Mixer { get { return _audioMixer; } set { _audioMixer = value; } }
    public void Init()
    {
        SetGroup(SoundTypes.BGM, Managers.UserData.data.BGM);
        SetGroup(SoundTypes.FX, Managers.UserData.data.FX);
    }
    public void Add(AudioSource source)
    {
        _sources.Add(source);

        if(_audioMixer == null)
        {
            Util.GetMonoBehaviour().StartCoroutine(SetOutputMixerGroup());
        }
        else
        {
            SetOutputMixerGroup(source);
        }
    }
    public void SetGroup(SoundTypes type)
    {
        if(type == _fx)
        {
            SetGroup(type, Managers.UserData.data.SetFX());
        }
        else
        {
            SetGroup(type, Managers.UserData.data.SetBGM());
        }
    }
    private void SetGroup(SoundTypes type, bool isActive)
    {
        float value;

        if(isActive)
        {
            if(type == _fx)
            {
                value = _maxValue_FX;
            }
            else
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
    private void SetOutputMixerGroup(AudioSource source)
    {
        if(source.outputAudioMixerGroup.ToString() == _fx.ToString())
        {
            source.outputAudioMixerGroup = _audioMixer.FindMatchingGroups("Master/FX")[0];
        }
        else
        {
            source.outputAudioMixerGroup = _audioMixer.FindMatchingGroups("Master/BGM")[0];
        }
    }
    private IEnumerator SetOutputMixerGroup()
    {
        yield return new WaitUntil(() => _audioMixer != null);

        SetOutputMixerGroup();
    }
}