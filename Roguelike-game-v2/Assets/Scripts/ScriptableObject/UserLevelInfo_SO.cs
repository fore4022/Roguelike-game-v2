using UnityEngine;
using System;
[CreateAssetMenu(fileName = "UserLevelInfo", menuName = "Create New SO/Create New UserLevelInfo_SO")]
public class UserLevelInfo_SO : ScriptableObject
{
    public static int maxLevel = 10;

    public int[] requiredEXP = new int[maxLevel];

    private void OnValidate()
    {
        Util.ResizeArray(ref requiredEXP, maxLevel);
    }
}