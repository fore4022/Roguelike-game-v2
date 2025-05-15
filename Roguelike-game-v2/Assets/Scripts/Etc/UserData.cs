using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class StageClearInfo
{
    public string name;
    public bool isClear;

    public StageClearInfo(string name, bool isClear)
    {
        this.name = name;
        this.isClear = isClear;
    }
}
public class UserData
{
    [SerializeField]
    private List<StageClearInfo> stageClearInfos = new();

    [SerializeField]
    private string current_StageName = "Prairie";
    [SerializeField]
    private int level = 1;
    [SerializeField]
    private int exp = 0;

    public List<StageClearInfo> StageClearInfo { get { return stageClearInfos; } set { stageClearInfos = value; } }
    public string StageName { get { return current_StageName; } set { current_StageName = value; } }
    public int Level { get { return level; } set { level = value; } }
    public int Exp { get { return exp; } set { exp = value; } }
    public bool isClear()
    {
        return stageClearInfos.Find(info => info.name == current_StageName).isClear;
    }
    public void Clear(string stageName)
    {
        stageClearInfos.Find(o => o.name == stageName).isClear = true;
    }
}