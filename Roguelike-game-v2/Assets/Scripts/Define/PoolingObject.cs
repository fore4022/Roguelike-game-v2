using UnityEngine;
public class PoolingObject
{
    public GameObject go;
    public bool isUsed = false;

    public Transform transform { get { return go.transform; } }
    public bool activeSelf { get { return go.activeSelf; } }
    public PoolingObject(GameObject go)
    {
        this.go = go;
    }
    public T GetComponent<T>()
    {
        return go.GetComponent<T>();
    }
    public void SetActive(bool active)
    {
        isUsed = false;

        go.SetActive(active);
    }
}