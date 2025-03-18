using UnityEngine;
using System;
[Serializable]
public class ProjectileInfo
{
    public string animationName = "";
    public float speed;
    public int penetration = 0;
}
[Serializable]
public class MultiCast
{
    public float[] delay;
    public float[] count;
}
[CreateAssetMenu(fileName = "Attack", menuName = "Create New SO/Create New Attack_SO")]
public class Attack_SO : ScriptableObject
{
    public const int maxLevel = 5;

    public ProjectileInfo projectile_Info;
    public MultiCast multiCast;

    public float[] damageCoefficient = new float[maxLevel];
    public float[] coolTime = new float[maxLevel];
    public float[] attackRange = new float[maxLevel];

    public Vector3 baseRotation;
    public string attackTypePath;
    public float duration;
    public bool flipX = false;
    public bool flipY = false;
    public bool projectile;
    public bool isMultiCast;

    private void OnValidate()
    {
        ResizeArray(ref damageCoefficient);
        ResizeArray(ref coolTime);
        ResizeArray(ref attackRange);

        if(isMultiCast)
        {
            ResizeArray(ref multiCast.delay);
            ResizeArray(ref multiCast.count);
        }
    }
    private void ResizeArray<T>(ref T[] array)
    {
        if(array.Length > maxLevel)
        {
            Array.Resize(ref array, maxLevel);
        }
    }
}