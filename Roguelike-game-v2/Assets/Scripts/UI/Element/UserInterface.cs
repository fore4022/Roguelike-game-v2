using UnityEngine;
/// <summary>
/// <para>
/// ��� UserInterface�� �⺻ ����
/// </para>
/// UserInterface�� ��ӹ��� Ŭ�������� UI_Manager�� ���ؼ� ���� ����
/// </summary>
public abstract class UserInterface : MonoBehaviour
{
    protected bool isInitalized = false;

    public bool IsInitalized { get { return isInitalized; } }
    // �ʱ�ȭ ���� �ʾ��� ��� UI_Manager�� ���, ���� Ȱ��ȭ�Ǵ� ��� Enable ����
    protected void OnEnable()
    {
        if(!isInitalized)
        {
            Managers.UI.Add(this);
        }
        else
        {
            Enable();
        }
    }
    // UI �ʱ�ȭ ȣ��
    protected virtual void Start()
    {
        InitUI();
    }
    // �ʱ�ȭ ���� UI�� Ȱ��ȭ ������ ���� ����
    protected virtual void Enable() { }
    // UI �ʱ�ȭ, UI_Manager�� �����ϵ��� ���� �޼��� ����
    public void InitUI()
    {
        if(!isInitalized)
        {
            isInitalized = true;

            SetUserInterface();
        }
    }
    // �ʱ�ȭ ����
    public abstract void SetUserInterface();
}