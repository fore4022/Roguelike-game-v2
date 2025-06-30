using UnityEngine;
public class PoolingObject
{
    public GameObject go;
    public bool isUsed = false;

    public PoolingObject(GameObject go)
    {
        this.go = go;
    }
}