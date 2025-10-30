using UnityEngine;
/// <summary>
/// <para>
/// ������Ʈ Ǯ�� ������ ������Ʈ�� ��� Ÿ��
/// </para>
/// Transform, Animator, SpriteRenderer, activeSelf, GetComponent, SetActive, GetType, StopAllCoroutines�� ��� ����
/// </summary>
public class PoolingObject
{
    public bool isInUse = false;
    public bool isUsed = false;

    private GameObject go;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public GameObject GameObject { get { return go; } }
    public Transform Transform { get { return go.transform; } }
    public Animator Animator { get { return GetType(ref animator); } }
    public SpriteRenderer SpriteRenderer { get { return GetType(ref spriteRenderer); } }
    public string Name { get { return go.name; } }
    // ���� ������Ʈ Ȱ��ȭ ���� ��ȯ, ���� ���� �� false ��ȯ
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
    // ���� ������Ʈ���� ������ Ÿ���� ������Ʈ �����ͼ� ��ȯ
    public T GetComponent<T>()
    {
        return go.GetComponent<T>();
    }
    // ���� ������Ʈ�� Ȱ��ȭ ���¸� ����, ���� ���� �Ǵ� ���� ������Ʈ�� �������� �ʴ� ��쿡 ������� ����
    public void SetActive(bool active)
    {
        if(go == null || Managers.Game.GameOver)
        {
            return;
        }

        isInUse = active;
        isUsed = true;

        go.SetActive(active);
    }
    // ���� ������Ʈ���� ����Ǵ� ��� �ڷ�ƾ�� �����Ѵ�.
    public void StopAllCoroutines()
    {
        go.GetComponent<MonoBehaviour>().StopAllCoroutines();
    }
    // variable�� null�� ��� ������ Ÿ���� ������Ʈ�� ������ �Ҵ�, �Ҵ�Ǿ� �ִٸ� ��� variable ��ȯ
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