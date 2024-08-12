using UnityEngine;
[CreateAssetMenu(fileName = "BasicAttack_SO", menuName = "Create New BasicAttack_SO")]
public class BasicAttack_SO : ScriptableObject
{
    public GameObject attackType;

    public string attackTypePath;
    public float damageCoefficient;
    public float attackSpeedCoefficient;
    public float attackRange;
    public float attackSize;
    public float duration;
}
