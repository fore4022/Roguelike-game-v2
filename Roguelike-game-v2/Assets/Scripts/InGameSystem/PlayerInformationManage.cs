public class PlayerInformationManage
{
    public PlayerInformation Info => Managers.Game.player.Information;
    public void AddExperience(float expAmount)
    {
        Info.Experience += Info.RequiredExperience;

        if (Info.Experience >= Info.RequiredExperience)
        {
            Info.Experience -= Info.RequiredExperience;
            Info.Level++;

            Info.SetRequiredExperience();
        }
    }
}