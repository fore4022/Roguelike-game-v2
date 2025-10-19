using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// <para>
/// ���� ����
/// </para>
/// �������� Ŭ���� ����, ����, �ɷ�ġ, ���������� �÷����� ��������, Ʃ�丮�� �÷��� ���θ� ����
/// </summary>
public class UserData
{
    [SerializeField]
    private List<StageClear_Information> stageClearInfos = new();
    [SerializeField]
    private Setting_Information _setting = new();
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
    [SerializeField]
    private bool tutorial = false;
    
    public List<StageClear_Information> StageClearInfo { get { return stageClearInfos; } set { stageClearInfos = value; } }
    public PlayerStat Stat { get { return _stat; } }
    public string StageName
    {
        get { return current_StageName; }
        set 
        {
            current_StageName = value;

            Managers.Data.Save();
        }
    }
    public int Level { get { return level; } set { level = value; } }
    public int Exp { get { return exp; } set { exp = value; } }
    public int StatPoint { get { return statPoint; } set { statPoint = value; } }
    public bool Tutorial { get { return tutorial; } set { tutorial = value; } }
    public bool BGM { get { return _setting.BGM; } }
    public bool FX { get { return _setting.FX; } }
    public StageState GetStageState()
    {
        return stageClearInfos.Find(info => info.name == current_StageName).state;
    }
    public bool SetBGM()
    {
        return _setting.BGM = !_setting.BGM;
    }
    public bool SetFX()
    {
        return _setting.FX = !_setting.FX;
    }
    public void Clear(string stageName)
    {
        StageClear_Information info = stageClearInfos.Find(o => o.name == stageName);

        if(info != null)
        {
            int index = stageClearInfos.IndexOf(info) + 1;

            if(index < stageClearInfos.Count)
            {
                if(stageClearInfos[index].state == StageState.Locked)
                {
                    stageClearInfos[index].state = StageState.Unlocked;
                }
            }

            info.state = StageState.Cleared;

            Managers.Data.Save();
        }
    }
}