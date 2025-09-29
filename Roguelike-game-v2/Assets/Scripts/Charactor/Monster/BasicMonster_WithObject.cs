/// <summary>
/// so를 통해서 추가적인 객체를 사용하는, 플레이어를 향해서 움직이는 기본 몬스터이다.
/// </summary>
public class BasicMonster_WithObject : BasicMonster
{
    protected new MonsterStat_WithObject_SO monsterSO { get { return base.monsterSO as MonsterStat_WithObject_SO; } }
}