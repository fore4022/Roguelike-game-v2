using UnityEngine;
[CreateAssetMenu(fileName = "Attack", menuName = "Create New SO/Create New Attack_SO")]
public class Attack_SO : ScriptableObject
{
    public float[] damageCoefficient;
    public float[] coolTime;
    public float[] attackRange;

    public string attackTypePath;
    public float kinematicDuration = 0.8f;
}