using UnityEngine;
/// <summary>
/// ��ȿ ȸ�� ����
/// </summary>
public class Monster_L : BasicMonster
{
    [SerializeField]
    private new float directionMultiplierDefault;

    protected override void Enable()
    {
        directionMultiplier = directionMultiplierDefault;

        base.Enable();
    }
}