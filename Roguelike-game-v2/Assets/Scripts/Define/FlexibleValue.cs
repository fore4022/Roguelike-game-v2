using UnityEngine;
public struct FlexibleValue
{
    private Vector3? vec;
    private float? f;

    public FlexibleValue(float f)
    {
        vec = null;
        this.f = f;
    }
    public FlexibleValue(Vector3 vec)
    {
        this.vec = vec;
        f = null;
    }
    public Vector3 Vector { get { return (Vector3)vec; } }
    public float Float { get { return (float)f; } }
}