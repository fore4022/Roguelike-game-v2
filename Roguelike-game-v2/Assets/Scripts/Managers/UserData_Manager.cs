using System.IO;
using UnityEngine;
public class UserData_Manager
{
    private UserData userData;

    private string filePath = "";

    public UserData GetUserData { get { return userData; } }
    public async void UserDataLoad()
    {
        if(filePath == string.Empty)
        {
            filePath = Application.persistentDataPath + "UserData.Json";
        }

        if(!File.Exists(filePath))
        {
            UserDataSave();
        }

        string jsonData = await File.ReadAllTextAsync(filePath);

        userData = JsonUtility.FromJson<UserData>(jsonData);
    }
    public async void UserDataSave()
    {
        if(userData == null)
        {
            userData = new();
        }

        string jsonData = JsonUtility.ToJson(userData);

        await File.WriteAllTextAsync(filePath, jsonData);
    }
}