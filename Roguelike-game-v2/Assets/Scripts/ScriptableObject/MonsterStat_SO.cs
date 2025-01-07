using UnityEngine;
[CreateAssetMenu(fileName = "BasicMonsterStat", menuName = "Create New SO/Create New BasicMonsterStat_SO")]
public class MonsterStat_SO : ScriptableObject
{
    public DefaultStat stat = new();

    public float experience;
}