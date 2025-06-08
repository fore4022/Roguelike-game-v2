using UnityEngine;
public class StatUpgrade_UI : UserInterface
{
    [SerializeField]
    private int sign;

    public FileReference file;

    public override void SetUserInterface()
    {
        Managers.UI.HideUI<StatUpgrade_UI>();
    }
    public void ChangeAmound()
    {
        //UI Update
    }
    public void Confirm()
    {
        Managers.UserData.Save();
        Managers.UI.HideUI<StatUpgrade_UI>();
    }
}