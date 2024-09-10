using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "UserLevelInfo", menuName = "Create UserLevelInfo_SO")]
public class UserLevelInfo_SO : ScriptableObject
{
    public List<UserLevel_SO> userLevelInfo;
}