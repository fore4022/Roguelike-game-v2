using UnityEngine;
[CreateAssetMenu(fileName = "BasicMonsterStat", menuName = "Create New SO/Create New BasicMonsterStat_SO")]
public class BasicMonsterStat_SO : ScriptableObject
{
    public DefaultStat stat = new();
    public CharactorInformation charactor = new();

    public float experience;
}