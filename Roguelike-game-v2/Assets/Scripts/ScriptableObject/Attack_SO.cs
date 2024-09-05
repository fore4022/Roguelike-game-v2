using UnityEngine;
[CreateAssetMenu(fileName = "Attack_SO", menuName = "Create New Attack_SO")]
public class Attack_SO : ScriptableObject
{
    public GameObject attackType;

    public string attackTypePath;
    public float damageCoefficient;
    public float attackSpeedCoefficient;
    public float attackRange;
    public float attackSize;
    public float duration;
}
