using UnityEngine;
[CreateAssetMenu(fileName = "Skill", menuName = "Create New SO/Skill/Create New Skill_SO")]
public class Skill_SO : ScriptableObject
{
    public const int maxLevel = 5;

    public ProjectileInfo projectile_Info;
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
        Util.ResizeArray(ref damageCoefficient, maxLevel);
        Util.ResizeArray(ref coolTime, maxLevel);

        if(isMultiCast)
        {
            Util.ResizeArray(ref multiCast.delay, maxLevel);
            Util.ResizeArray(ref multiCast.count, maxLevel);
        }
    }
#endif
}