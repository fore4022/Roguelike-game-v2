using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
public class Audio_Manager
{
    private AudioMixer _audioMixer;

    private const SoundTypes _fx = SoundTypes.FX;
    private const float _maxValue_BGM = -5;
    private const float _maxValue_FX = 2.5f;
    private const float _minValue = -80;
    
    public AudioMixer Mixer { get { return _audioMixer; } set { _audioMixer = value; } }
    public void Init()
    {
        SetGroup(SoundTypes.BGM, Managers.Data.data.BGM);
        SetGroup(SoundTypes.FX, Managers.Data.data.FX);
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
            Util.GetMonoBehaviour().StartCoroutine(WaitForAudioMixer(source, type));
        }
        else
        {
            SetOutputMixerGroup(source, type);
        }
    }
    public void SetGroup(SoundTypes type)
    {
        if(type == _fx)
        {
            SetGroup(type, Managers.Data.data.SetFX());
        }
        else
        {
            SetGroup(type, Managers.Data.data.SetBGM());
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
    public void SetOutputMixerGroup(AudioSource source, SoundTypes? type = null)
    {
        if(source.outputAudioMixerGroup.ToString() == _fx.ToString() || type == _fx)
        {
            source.outputAudioMixerGroup = _audioMixer.FindMatchingGroups("Master/FX")[0];
        }
        else
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

        SetOutputMixerGroup(source);
    }
}