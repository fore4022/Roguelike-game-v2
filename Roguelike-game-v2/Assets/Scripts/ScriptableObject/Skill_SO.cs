using UnityEngine;
[CreateAssetMenu(fileName = "Skill", menuName = "Create New SO/Skill/Create New Skill_SO")]
public class Skill_SO : ScriptableObject
{
    public const int maxLevel = 5;

    public Projectile_Information projectile_Info;
    public MultiCast multiCast;

    public GameObject go;

    public Color maxLevelColor = default;
    public Vector3 adjustmentRotation;
    public Vector2 adjustmentPosition;
    public float[] damageCoefficient = new float[maxLevel];
    public float[] coolTime = new float[maxLevel];
    public float duration;
    public bool flipX = false;
    public bool flipY = false;
    public bool isProjectile;
    public bool isMultiCast;

    public string typePath { get { return go.name; } }
#if UNITY_EDITOR
    private void OnValidate()
    {
        ArrayUtil.ResizeArray(ref damageCoefficient, maxLevel);
        ArrayUtil.ResizeArray(ref coolTime, maxLevel);

        if(isMultiCast)
        {
            ArrayUtil.ResizeArray(ref multiCast.delay, maxLevel);
            ArrayUtil.ResizeArray(ref multiCast.count, maxLevel);
        }
    }
#endif
}