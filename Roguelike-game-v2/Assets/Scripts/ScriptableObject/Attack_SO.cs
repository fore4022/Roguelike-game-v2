using UnityEngine;
using System;
[Serializable]
public class ProjectileInfo
{
    public float speed;
    public int Penetration;
}
[CreateAssetMenu(fileName = "Attack", menuName = "Create New SO/Create New Attack_SO")]
public class Attack_SO : ScriptableObject
{
    public static int maxLevel = 5;

    public ProjectileInfo projectile_Info;

    public float[] damageCoefficient = new float[maxLevel];
    public float[] coolTime = new float[maxLevel];
    public float[] attackRange = new float[maxLevel];

    public string attackTypePath;
    public float kinematicDuration = 0.8f;
    public bool projectile;

    public int MaxLevel { get { return maxLevel; } }
    private void OnValidate()
    {
        if(damageCoefficient.Length > maxLevel)
        {
            Array.Resize(ref damageCoefficient, maxLevel);
        }

        if(coolTime.Length > maxLevel)
        {
            Array.Resize(ref coolTime, maxLevel);
        }

        if(attackRange.Length > maxLevel)
        {
            Array.Resize(ref attackRange, maxLevel);
        }
    }
}