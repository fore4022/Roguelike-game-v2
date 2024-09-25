public class PlayerInformationManage
{
    public PlayerInformation info => Managers.Game.player.Information;

    public void AddExperience(float expAmount)
    {
        info.Experience += expAmount;

        if (info.Experience >= info.RequiredExperience)
        {
            info.Experience -= info.RequiredExperience;
            info.Level++;

            info.SetRequiredExperience();
        }
    
}