using System.IO;
using UnityEngine;
public class UserData_Manager
{
    public UserData data;

    private string filePath = "";
    
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