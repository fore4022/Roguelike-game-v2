using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "UserLevel", menuName = "Create New SO/Create New UserLevel_SO")]
public class UserLevel_SO : ScriptableObject
{
    public List<GameObject> skillList;
    public List<AttackInformation_SO> attackInformationList;
}