using UnityEngine;
public struct NumericValue
{
    private Vector3? _vec;
    private float? _f;

    public NumericValue(float f)
    {
        _vec = null;
        _f = f;
    }
    public NumericValue(Vector3 vec)
    {
        _vec = vec;
        _f = null;
    }
    public Vector3 Vector { get { return (Vector3)_vec; } set { _vec = value; } }
    public float Float { get { return (float)_f; } set { _f = value; } }
}