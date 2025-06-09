using TMPro;
public class StatElementUpgrade_UI : UserInterface
{
    public TextMeshProUGUI tmp;

    public override void SetUserInterface()
    {
        tmp = Util.GetComponentInChildren<TextMeshProUGUI>(transform);

        // Get Index to StatUpgrade_UI
        // -, + button state setting
    }
    public void ChangeAmount(int sign)
    {
        //file.SetValue((float)file.GetValue() - sign);

        //UnityEngine.Debug.Log(file.GetValue());
    }
}