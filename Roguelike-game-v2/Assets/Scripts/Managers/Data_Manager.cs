using System.IO;
using UnityEngine;
/// <summary>
/// <para>
/// ������ �����͸� ����, ����, ����, �ҷ����� ��� ����
/// </para>
/// �����ʹ� JSON �������� ����̽� ȯ���� ���� ������ ��ġ
/// </summary>
public class Data_Manager
{
    public UserData user = null;

    private UserExpTable_SO userExpTable;

    private const string userExpTablePath = "UserExpTable";

    private string filePath = "";
    
    public UserExpTable_SO UserExpTable { get { return userExpTable; } }
    // ���� ������ ����ġ ǥ�� �ҷ�����, ���� ������ ���� ��� ����
    public async void Load()
    {
        userExpTable = await AddressableHelper.LoadingToPath<UserExpTable_SO>(userExpTablePath);

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
    // ������ ���� ��� �⺻ ���·� ����
    public async void Save()
    {
        if(user == null)
        {
            user = new();

            foreach(Stage_SO so in Managers.Main.stageDatas.Icons)
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