using TMPro;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class StatElementUpgrade_UI : UserInterface
{
    public TextMeshProUGUI tmp;
    public AudioSource audioSource;
    public GameObject inc;
    public GameObject dec;

    private FileReference file;

    private const string log1 = "You are lacking stat points.";
    private const string log2 = "Stat points cannot be used.";

    private float value;

    public override void SetUserInterface()
    {
        tmp = Util.GetComponentInChildren<TextMeshProUGUI>(transform, true);
        audioSource = GetComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }
    public void Set(FileReference file)
    {
        this.file = file;
        value = (float)file.GetValue();

        ChangeAmount(0);
    }
    public void ChangeAmount(int sign)
    {
        if(!LacksStatPoints(sign) || !CanUseStatPoints(sign))
        {
            AudioPlay(0);

            return;
        }

        if(value == 0 && sign == 1)
        {
            dec.SetActive(true);
        }
        else if(value == Stat_Manage.maxLevel && sign == -1)
        {
            inc.SetActive(true);
        }

        if(sign != 0)
        {
            AudioPlay(sign);
        }

        value += sign;
        tmp.text = $"+ {value}";
        Managers.Data.data.StatPoint -= sign;

        file.SetValue(value);
        Managers.UI.Get<StatUpgrade_UI>().TextUpdate();

        if(value == 0)
        {
            dec.SetActive(false);
        }
        else if(value == Stat_Manage.maxLevel)
        {
            inc.SetActive(false);
        }
    }
    private bool LacksStatPoints(int sign)
    {
        if(Managers.Data.data.StatPoint == 0)
        {
            if(sign == 1 || (sign == -1 && value == 0))
            {
                Managers.UI.ShowAndGet<ToastMessage_UI>().SetText(log1);

                return false;
            }
        }

        return true;
    }
    private bool CanUseStatPoints(int sign)
    {
        if((value == 0 && sign == -1) || (value == Stat_Manage.maxLevel && sign == 1))
        {
            Managers.UI.ShowAndGet<ToastMessage_UI>().SetText(log2);

            return false;
        }

        return true;
    }
    private void AudioPlay(int sign)
    {
        if(sign == 0)
        {
            audioSource.clip = Managers.UI.Get<StatUpgrade_UI>().ActionUnavailableSound;
        }
        else if(sign == 1)
        {
            audioSource.clip = Managers.UI.Get<StatUpgrade_UI>().IncreaseSound;
        }
        else if(sign == -1)
        {
            audioSource.clip = Managers.UI.Get<StatUpgrade_UI>().DecreaseSound;
        }

        audioSource.Play();
    }
}