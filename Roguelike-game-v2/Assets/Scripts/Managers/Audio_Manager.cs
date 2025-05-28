using UnityEngine.Audio;
public class Audio_Manager
{
    private AudioMixer audioMixer;

    private const float maxValue = -5;
    private const float minValue = -80;

    public AudioMixer Mixer { get { return audioMixer; } set { audioMixer = value; } }
    public void Init()
    {
        SetGroup(SoundTypes.BGM, Managers.UserData.data.BGM);
        SetGroup(SoundTypes.FX, Managers.UserData.data.FX);
    }
    public void SetGroup(SoundTypes type)
    {
        switch(type)
        {
            case SoundTypes.BGM:
                SetGroup(type, Managers.UserData.data.SetBGM());
                break;
            case SoundTypes.FX:
                SetGroup(type, Managers.UserData.data.SetFX());
                break;
        }
    }
    public void SetGroup(SoundTypes type, bool isActive)
    {
        float value;

        if(isActive)
        {
            value = maxValue;
        }
        else
        {
            value = minValue;
        }

        audioMixer.SetFloat(type.ToString(), value);
    }
}