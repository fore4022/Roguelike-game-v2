using UnityEngine;
public class StatUpgrade_UI : UserInterface
{
    [HideInInspector]
    public PlayerStat stat;

    private FileReference file;

    public override void SetUserInterface()
    {
        Managers.UI.HideUI<StatUpgrade_UI>();
    }
    public void Confirm()
    {
        Managers.UserData.Save();
        Managers.UI.HideUI<StatUpgrade_UI>();
    }
}