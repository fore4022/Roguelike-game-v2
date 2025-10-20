using UnityEngine;
/// <summary>
/// <para>
/// 모든 UserInterface의 기본 구현
/// </para>
/// UserInterface를 상속받은 클래스들은 UI_Manager를 통해서 접근 가능
/// </summary>
public abstract class UserInterface : MonoBehaviour
{
    protected bool isInitalized = false;

    public bool IsInitalized { get { return isInitalized; } }
    public void SetUI()
    {
        if(!isInitalized)
        {
            isInitalized = true;

            SetUserInterface();
        }
    }
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
    protected virtual void Start()
    {
        SetUI();
    }
    protected virtual void Enable() { }
    public abstract void SetUserInterface();
}