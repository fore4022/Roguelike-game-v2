using System.Collections.Generic;
using UnityEngine;
public class UserData
{
    [SerializeField]
    private List<StageClearInfo> stageClearInfos = new();
    [SerializeField]
    private SettingInformation setting = new();

    [SerializeField]
    private string current_StageName = "Prairie";
    [SerializeField]
    private int level = 1;
    [SerializeField]
    private int exp = 0;

    public List<StageClearInfo> StageClearInfo { get { return stageClearInfos; } set { stageClearInfos = value; } }
    public string StageName
    {
        get
        {
            return current_StageName;
        }
        set 
        {
            current_StageName = value;

            Managers.UserData.Save();
        }
    }
    public int Level { get { return level; } set { level = value; } }
    public int Exp { get { return exp; } set { exp = value; } }
    public bool BGM { get { return setting._BGM; } }
    public bool SFX { get { return setting._SFX; } }
    public StageState GetStageState()
    {
        return stageClearInfos.Find(info => info.name == current_StageName).state;
    }
    public void Clear(string stageName)
    {
        StageClearInfo info = stageClearInfos.Find(o => o.name == stageName);

        if(info != null)
        {
            info.state = StageState.Cleared;

            Managers.UserData.Save();
        }
    }
    public void SetBGM()
    {
        setting._BGM = !setting._BGM;
    }
    public void SetSFX()
    {
        setting._SFX = !setting._SFX;
    }
}