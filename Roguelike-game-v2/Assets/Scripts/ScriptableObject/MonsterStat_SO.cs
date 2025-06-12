using UnityEngine;
[CreateAssetMenu(fileName = "BasicMonsterStat", menuName = "Create New SO/Create New BasicMonsterStat_SO")]
public class MonsterStat_SO : ScriptableObject
{
    public DefaultStat stat;
    public GameObject extraObject;

    public int user_Experience;
    public int inGame_Experience;
    public bool hasExtraObject;
}