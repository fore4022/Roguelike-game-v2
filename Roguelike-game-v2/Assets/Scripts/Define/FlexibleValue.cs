using UnityEngine;
public struct FlexibleValue
{
    private Vector3? _vec;
    private float? _f;

    public FlexibleValue(float f)
    {
        _vec = null;
        this._f = f;
    }
    public FlexibleValue(Vector3 vec)
    {
        this._vec = vec;
        _f = null;
    }
    public Vector3 Vector { get { return (Vector3)_vec; } set { _vec = value; } }
    public float Float { get { return (float)_f; } set { _f = value; } }
}