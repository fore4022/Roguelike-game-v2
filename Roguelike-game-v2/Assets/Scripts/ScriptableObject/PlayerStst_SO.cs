using UnityEngine;
[CreateAssetMenu(fileName = "PlayerStat_SO", menuName = "Create New SO/Create New PlayerStat_SO")]
public class PlayerStat_SO : ScriptableObject
{
    public float health;
    public float damage;
    public float attackSpeed;
    public float moveSpeed;
    public float size;
    public float dieing_AnimationDuration;
}
