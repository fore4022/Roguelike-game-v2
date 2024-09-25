public class PlayerInformationManage
{
    public PlayerInformation info = null;

    private void Init()
    {
        if(info == null)
        {
            UnityEngine.Debug.Log(Managers.Game.player.Information);
            UnityEngine.Debug.Log(info);

            info = Managers.Game.player.Information;
        }
    }
    public void AddExperience(float expAmount)
    {
        Init();

        info.Experience += expAmount;

        if (info.Experience >= info.RequiredExperience)
        {
            info.Experience -= info.RequiredExperience;
            info.Level++;

            info.SetRequiredExperience();
        }
    }
}