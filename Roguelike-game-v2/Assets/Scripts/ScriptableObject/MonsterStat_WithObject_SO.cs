using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MonsterStat_WithObject", menuName = "Create New SO/Create New MonsterStat_WithObject_SO")]
public class MonsterStat_WithObject_SO : MonsterStat_SO
{
    public List<GameObject> extraObjects;

    public bool hasExtraObject;
}