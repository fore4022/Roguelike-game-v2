using TMPro;
using UnityEngine;
public class StatElementUpgrade_UI : UserInterface
{
    public TextMeshProUGUI tmp;
    public GameObject inc;
    public GameObject dec;

    private FileReference file;

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
        int value = (int)file.GetValue();

        if((value == 0 && sign == -1) || (value == StatSelection.maxLevel && sign == 1))
        {
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