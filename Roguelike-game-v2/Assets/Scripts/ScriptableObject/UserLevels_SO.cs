using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "UserLevels", menuName = "Create New SO/Create New UserLevels_SO")]
public class UserLevels_SO : ScriptableObject
{
    public List<UserLevel_SO> levelInfo;

    public int count;
}