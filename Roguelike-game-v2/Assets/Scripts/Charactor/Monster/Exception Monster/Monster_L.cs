using UnityEngine;
/// <summary>
/// 방향 전환이 잘 안되는 몬스터이다.
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