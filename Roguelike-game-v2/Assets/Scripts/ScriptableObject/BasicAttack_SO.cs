using UnityEngine;
[CreateAssetMenu(fileName = "BasicAttack_SO", menuName = "Create New BasicAttack_SO")]
public class BasicAttack_SO : ScriptableObject
{
    public GameObject attackType;
    public float damageCoefficient;
    public float attackSpeedCoefficient;
    public float attackSize;
    public float duration;
}
