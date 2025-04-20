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
    private List<StageClearInfo> stageClearInfoList = new();

    [SerializeField]
    private string current_StageName = "Prairie";
    [SerializeField]
    private int level = 1;
    [SerializeField]
    private int exp = 0;

    public List<StageClearInfo> StageClearInfo { get { return stageClearInfoList; } set { stageClearInfoList = value; } }
    public string StageName { get { return current_StageName; } set { current_StageName = value; } }
    public int Level { get { return level; } set { level = value; } }
    public int Exp { get { return exp; } set { exp = value; } }
    public bool isClear()
    {
        foreach(StageClearInfo info in stageClearInfoList)
        {
            Debug.Log(info.name == current_StageName);
        }

        Debug.Log("-----");
        Debug.Log(current_StageName);
        Debug.Log("-----");

        return stageClearInfoList.Find(info => info.name == current_StageName).isClear;
    }
}