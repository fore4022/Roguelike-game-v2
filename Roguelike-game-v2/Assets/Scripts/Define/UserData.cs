using System.Collections.Generic;
using UnityEngine;
public class UserData
{
    [SerializeField]
    private Dictionary<string, bool> stageClearInfo = new();

    [SerializeField]
    private string current_StageName = "Prairie";
    [SerializeField]
    private int level = 5;
    [SerializeField]
    private int exp = 0;

    public Dictionary<string, bool> StageClearInfo { get { return stageClearInfo; } set { stageClearInfo = value; } }
    public string StageName { get { return current_StageName; } set { current_StageName = value; } }
    public int Level { get { return level; } set { level = value; } }
    public int Exp { get { return exp; } set { exp = value; } }
    public bool isClear()
    {
        return stageClearInfo[current_StageName];
    }
}