using TMPro;
using UnityEngine;
public class StatElementUpgrade_UI : UserInterface
{
    public TextMeshProUGUI tmp;
    public GameObject inc;
    public GameObject dec;

    private FileReference file;

    private const string log1 = "You are lacking stat points.";
    private const string log2 = "Stat points cannot be used.";

    public override void SetUserInterface()
    {
        tmp = Util.GetComponentInChildren<TextMeshProUGUI>(transform, true);
    }
    public void Set(FileReference file)
    {
        this.file = file;

        ChangeAmount(0);
    }
    public void ChangeAmount(int sign)
    {
        if(Managers.UserData.data.Stat.statPoint == 0)
        {
            if(sign != 0)
            {
                Managers.UI.ShowAndGet<ToastMessage_UI>().SetText(log1);

                return;
            }
        }

        int value = (int)file.GetValue();

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

        Managers.UserData.data.Stat.statPoint -= sign;
        value += sign;
        tmp.text = $"+ {value}";

        file.SetValue(value);

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