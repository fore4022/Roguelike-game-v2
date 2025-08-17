using System.IO;
using UnityEngine;
public class UserData_Manager
{
    public UserData data = null;

    private UserLevelInfo_SO userLevelInfo;

    private const string userLevelInfoPath = "UserLevelInfo_SO";

    private string filePath = "";
    
    public UserLevelInfo_SO UserLevelInfo { get { return userLevelInfo; } }
    public async void Load()
    {
        userLevelInfo = Util.LoadingToPath<UserLevelInfo_SO>(userLevelInfoPath);

        if(filePath == string.Empty)
        {
            filePath = Application.persistentDataPath + "UserData.Json";
        }

        if(!File.Exists(filePath))
        {
            Save();

            return;
        }

        data = JsonUtility.FromJson<UserData>(await File.ReadAllTextAsync(filePath));
    }
    public async void Save()
    {
        if(data == null)
        {
            data = new();

            foreach(Stage_SO so in Managers.Main.GameData.Stages)
            {
                if(Managers.UserData.data.StageClearInfo.Find(info => info.name == so.stagePath) == null)
                {
                    if(data.StageClearInfo.Count == 0)
                    {
                        data.StageClearInfo.Add(new(so.stagePath, StageState.Unlocked));
                    }
                    else
                    {
                        data.StageClearInfo.Add(new(so.stagePath, StageState.Locked));
                    }
                }
            }
        }

        await File.WriteAllTextAsync(filePath, JsonUtility.ToJson(data));
    }
}