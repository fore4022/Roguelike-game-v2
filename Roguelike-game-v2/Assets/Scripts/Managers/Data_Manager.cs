using System.IO;
using UnityEngine;
public class Data_Manager
{
    public UserData user = null;

    private UserLevelData_SO userLevelInfo;

    private const string userLevelInfoPath = "UserLevelInfo_SO";

    private string filePath = "";
    
    public UserLevelData_SO UserLevelInfo { get { return userLevelInfo; } }
    public async void Load()
    {
        userLevelInfo = await Util.LoadingToPath<UserLevelData_SO>(userLevelInfoPath);

        if(filePath == string.Empty)
        {
            filePath = Application.persistentDataPath + "UserData.Json";
        }

        if(!File.Exists(filePath))
        {
            Save();

            return;
        }

        user = JsonUtility.FromJson<UserData>(await File.ReadAllTextAsync(filePath));
    }
    public async void Save()
    {
        if(user == null)
        {
            user = new();

            foreach(Stage_SO so in Managers.Main.stageDatas.Stages)
            {
                if(Managers.Data.user.StageClearInfo.Find(info => info.name == so.stagePath) == null)
                {
                    if(user.StageClearInfo.Count == 0)
                    {
                        user.StageClearInfo.Add(new(so.stagePath, StageState.Unlocked));
                    }
                    else
                    {
                        user.StageClearInfo.Add(new(so.stagePath, StageState.Locked));
                    }
                }
            }
        }

        await File.WriteAllTextAsync(filePath, JsonUtility.ToJson(user));
    }
}