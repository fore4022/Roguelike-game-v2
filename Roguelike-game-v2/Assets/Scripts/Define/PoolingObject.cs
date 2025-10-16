using UnityEngine;
/// <summary>
/// <para>
/// ������Ʈ Ǯ�� ������ ������Ʈ�� ��� Ÿ��
/// </para>
/// Transform, Animator, SpriteRenderer, activeSelf, GetComponent, SetActive, GetType, StopAllCoroutines�� ����� �� �ִ�.
/// </summary>
public class PoolingObject
{
    public bool isUsed = false;

    private GameObject go;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public GameObject PoolingGameObject { get { return go; } }
    public Transform Transform { get { return go.transform; } }
    public Animator Animator { get { return GetType(ref animator); } }
    public SpriteRenderer SpriteRenderer { get { return GetType(ref spriteRenderer); } }
    public bool activeSelf 
    {
        get 
        {
            if(go == null || Managers.Game.GameOver)
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
        if(go == null || Managers.Game.GameOver)
        {
            return;
        }

        isUsed = active;

        go.SetActive(active);
    }
    public void StopAllCoroutines()
    {
        go.GetComponent<MonoBehaviour>().StopAllCoroutines();
    }
    private T GetType<T>(ref T variable)
    {
        if(variable != null)
        {
            return variable;
        }

        return variable = go.GetComponent<T>();
    }
    public PoolingObject(GameObject go)
    {
        this.go = go;
    }
}