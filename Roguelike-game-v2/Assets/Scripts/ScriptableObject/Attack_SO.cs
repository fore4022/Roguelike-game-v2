using UnityEngine;
[CreateAssetMenu(fileName = "Attack", menuName = "Create New SO/Create New Attack_SO")]
public class Attack_SO : ScriptableObject
{
    public string attackTypePath;
    public float damageCoefficient;
    public float attackSpeedCoefficient;
    public float attackRange;
    public float attackSize;
    public float kinematicDuration = 0.8f;
}