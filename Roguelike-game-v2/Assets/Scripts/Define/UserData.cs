using System.Collections.Generic;
using UnityEngine;
public class UserData
{
    [SerializeField]
    private List<StageClearInfo> stageClearInfos = new();
    [SerializeField]
    private SettingInformation _setting = new();
    [SerializeField]
    private PlayerStat _stat = new();

    [SerializeField]
    private string current_StageName = "Prairie";
    [SerializeField]
    private int level = 1;
    [SerializeField]
    private int exp = 0;
    [SerializeField]
    private int statPoint = 1;

    public List<StageClearInfo> StageClearInfo { get { return stageClearInfos; } set { stageClearInfos = value; } }
    public PlayerStat Stat { get { return _stat; } }
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
    public int StatPoint { get { return statPoint; } set { statPoint = value; } }
    public bool BGM { get { return _setting.BGM; } }
    public bool FX { get { return _setting.FX; } }
    public StageState GetStageState()
    {
        return stageClearInfos.Find(info => info.name == current_StageName).state;
    }
    public void Clear(string stageName)
    {
        StageClearInfo info = stageClearInfos.Find(o => o.name == stageName);

        if(info != null)
        {
            int index = stageClearInfos.IndexOf(info) + 1;

            if(index < stageClearInfos.Count)
            {
                stageClearInfos[index].state = StageState.Unlocked;
            }

            info.state = StageState.Cleared;

            Managers.UserData.Save();
        }
    }
    public bool SetBGM()
    {
        _setting.BGM = !_setting.BGM;

        return _setting.BGM;
    }
    public bool SetFX()
    {
        _setting.FX = !_setting.FX;

        return _setting.FX;
    }
}