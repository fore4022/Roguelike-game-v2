using UnityEngine;
public class PoolingObject
{
    public GameObject go;
    public SpriteRenderer _render = null;
    public bool isUsed = false;

    public Transform transform { get { return go.transform; } }
    public SpriteRenderer render
    {
        get
        {
            if(_render == null)
            {
                _render = go.GetComponent<SpriteRenderer>();
            }

            return _render;
        }
    }
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