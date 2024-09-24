using UnityEngine;
[CreateAssetMenu(fileName = "PlayerStat_SO", menuName = "Create New SO/Create New PlayerStat_SO")]
public class PlayerStat_SO : ScriptableObject
{
    public DefaultStat stat = new();
    public CharactorInformation charactor = new();
}