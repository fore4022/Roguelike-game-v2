using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "UserLevelInfo", menuName = "Create New SO/Create New UserLevelInfo_SO")]
public class UserLevelInfo_SO : ScriptableObject
{
    public List<UserLevel_SO> LevelInfo;
}