using UnityEngine.Audio;
public class Audio_Manager
{
    private AudioMixer audioMixer;

    private const float maxValue_BGM = -5;
    private const float maxValue_FX = 2.5f;
    private const float minValue = -80;

    public AudioMixer Mixer { get { return audioMixer; } set { audioMixer = value; } }
    public void Init()
    {
        SetGroup(SoundTypes.BGM, Managers.UserData.data.BGM);
        SetGroup(SoundTypes.FX, Managers.UserData.data.FX);
    }
    public void SetGroup(SoundTypes type)
    {
        if(type == SoundTypes.BGM)
        {
            SetGroup(type, Managers.UserData.data.SetBGM());
        }
        else
        {
            SetGroup(type, Managers.UserData.data.SetFX());
        }
    }
    public void SetGroup(SoundTypes type, bool isActive)
    {
        float value;

        if(isActive)
        {
            if(type == SoundTypes.FX)
            {
                value = maxValue_FX;
            }
            else
            {
                value = maxValue_BGM;
            }
        }
        else
        {
            value = minValue;
        }

        audioMixer.SetFloat(type.ToString(), value);
    }
}