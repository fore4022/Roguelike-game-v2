using TMPro;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class StatElementUpgrade_UI : UserInterface
{
    [SerializeField]
    private AudioClip increaseSound;
    [SerializeField]
    private AudioClip decreaseSound;
    [SerializeField]
    private AudioClip blockedSound;

    public TextMeshProUGUI tmp;
    public AudioSource audioSource;
    public GameObject inc;
    public GameObject dec;

    private FileReference file;

    private const string log1 = "You are lacking stat points.";
    private const string log2 = "Stat points cannot be used.";

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

        ChangeAmount(0);
    }
    public void ChangeAmount(int sign)
    {
        int value = (int)file.GetValue();

        if(Managers.UserData.data.StatPoint == 0)
        {
            if(sign == 1 || (sign == -1 && value == 0))
            {
                Managers.UI.ShowAndGet<ToastMessage_UI>().SetText(log1);

                return;
            }
        }

        if((value == 0 && sign == -1) || (value == StatSelection.maxLevel && sign == 1))
        {
            Managers.UI.ShowAndGet<ToastMessage_UI>().SetText(log2);

            return;
        }

        if(value == 0 && sign == 1)
        {
            dec.SetActive(true);
        }
        else if(value == StatSelection.maxLevel && sign == -1)
        {
            inc.SetActive(true);
        }

        value += sign;
        tmp.text = $"+ {value}";
        Managers.UserData.data.StatPoint -= sign;

        file.SetValue(value);
        Managers.UI.Get<StatUpgrade_UI>().TextUpdate();

        if(value == 0)
        {
            dec.SetActive(false);
        }
        else if(value == StatSelection.maxLevel)
        {
            inc.SetActive(false);
        }
    }
}