using System.IO;
using UnityEngine;
public class UserData_Manager
{
    public UserData data;

    private UserLevelInfo_SO userLevelInfo;

    private const string userLevelInfoPath = "UserLevelInfo_SO";

    private string filePath = "";
    
    public UserLevelInfo_SO UserLevelInfo { get { return userLevelInfo; } }
    public async void UserDataLoad()
    {
        userLevelInfo = await Util.LoadingToPath<UserLevelInfo_SO>(userLevelInfoPath);

        if(filePath == string.Empty)
        {
            filePath = Application.persistentDataPath + "UserData.Json";
        }

        if(!File.Exists(filePath))
        {
            UserDataSave();

            return;
        }

        string jsonData = await File.ReadAllTextAsync(filePath);

        data = JsonUtility.FromJson<UserData>(jsonData);
    }
    public async void UserDataSave()
    {
        if(data == null)
        {
            data = new();
        }

        string jsonData = JsonUtility.ToJson(data);

        await File.WriteAllTextAsync(filePath, jsonData);
    }
}