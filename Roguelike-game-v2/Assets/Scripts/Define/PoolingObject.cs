using UnityEngine;
public class PoolingObject
{
    public bool isUsed = false;

    private GameObject go;
    private Animator animator;
    private SpriteRenderer spriteRenderer = null;

    public GameObject PoolingGameObject { get { return go; } }
    public Transform Transform { get { return go.transform; } }
    public Animator Animator { get { return GetType(ref animator); } }
    public SpriteRenderer SpriteRenderer { get { return GetType(ref spriteRenderer); } }
    public bool activeSelf 
    {
        get 
        {
            if(ReferenceEquals(go, null) || Managers.Game.GameOver)
            {
                return false;
            }
            else
            {
                return go.activeSelf;
            }
        } 
    }
    public T GetComponent<T>()
    {
        return go.GetComponent<T>();
    }
    public void SetActive(bool active)
    {
        isUsed = active;

        go.SetActive(active);
    }
    private T GetType<T>(ref T variable)
    {
        return variable = go.GetComponent<T>();
    }
    public PoolingObject(GameObject go)
    {
        this.go = go;
    }
}