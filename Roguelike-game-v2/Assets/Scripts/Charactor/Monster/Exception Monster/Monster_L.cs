using UnityEngine;
/// <summary>
/// ���� ��ȯ�� �� �ȵǴ� �����̴�.
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