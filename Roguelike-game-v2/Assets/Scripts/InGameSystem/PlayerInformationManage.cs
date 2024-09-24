public class PlayerInformationManage
{
    public PlayerInformation Info { get { return Managers.Game.player.Information; } }
    public void AddExperience(float expAmount)
    {
        Info.experience += expAmount;

        if(Info.experience >= Info.basicRequiredExperience)
        {
            Info.experience -= Info.basicRequiredExperience;
            Info.level++;
        }
    }
}