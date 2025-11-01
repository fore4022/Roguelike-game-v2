using System.IO;
using UnityEngine;
/// <summary>
/// <para>
/// 유저의 데이터를 생성, 수정, 저장, 불러오기 기능 제공
/// </para>
/// 데이터는 JSON 형식으로 디바이스 환경의 저장 공간에 위치
/// </summary>
public class Data_Manager
{
    public UserData user = null;

    private UserExpTable_SO userExpTable;

    private const string userExpTablePath = "UserExpTable";

    private string filePath = "";
    
    public UserExpTable_SO UserExpTable { get { return userExpTable; } }
    // 유저 정보와 경험치 표를 불러오며, 유저 정보가 없을 경우 생성
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
    // 정보가 없을 경우 기본 상태로 저장
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