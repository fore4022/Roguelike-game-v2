using UnityEngine;
/// <summary>
/// <para>
/// 모든 UserInterface의 기본 구현이다.
/// </para>
/// UserInterface를 상속받은 클래스들은 Managers.UI를 통해서 접근할 수 있다.
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