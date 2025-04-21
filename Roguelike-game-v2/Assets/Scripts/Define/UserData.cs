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
        Debug.Log(current_StageName == null);
        Debug.Log(current_StageName);

        Debug.Log("---");

        foreach (StageClearInfo s in stageClearInfoList)
        {
            Debug.Log(s.name);
        }

        for(int i = 0; i < current_StageName.Length; i++)
        {
            Debug.Log(current_StageName[i]);
        }

        Debug.Log(current_StageName.Length);

        Debug.Log(stageClearInfoList.Find(info => info.name == current_StageName));

        return true;
    }
}