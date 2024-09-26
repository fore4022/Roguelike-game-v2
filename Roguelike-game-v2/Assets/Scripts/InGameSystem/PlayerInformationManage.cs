using System;
public class PlayerInformationManage
{
    public Action<float> experienceUpdate = null;
    public Action<int> levelUpdate = null;

    private PlayerInformation info = null;
    
    public PlayerInformation Info { set { info = value; } }
}
/*
if (info.Experience >= info.RequiredExperience)
{
    info.Experience -= info.RequiredExperience;
    info.Level++;

    info.SetRequiredExperience();
}*/