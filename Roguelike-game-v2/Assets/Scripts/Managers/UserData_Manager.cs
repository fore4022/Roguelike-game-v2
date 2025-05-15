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
            Debug.Log("a");

            Save();

            return;
        }

        //string jsonData = await File.ReadAllTextAsync(filePath);

        //data = JsonUtility.FromJson<UserData>(jsonData);
        data = JsonUtility.FromJson<UserData>(await File.ReadAllTextAsync(filePath));
    }
    public async void Save()
    {
        if(data == null)
        {
            Debug.Log("create");

            data = new();

            foreach(Stage_SO so in Managers.Main.GameData.Stages)
            {
                if(Managers.UserData.data.StageClearInfo.Find(info => info.name == so.stagePath) == null)
                {
                    data.StageClearInfo.Add(new StageClearInfo(so.stagePath, false));
                }
            }
        }

        //string jsonData = JsonUtility.ToJson(data);

        //await File.WriteAllTextAsync(filePath, jsonData);
        await File.WriteAllTextAsync(filePath, JsonUtility.ToJson(data));
    }
}