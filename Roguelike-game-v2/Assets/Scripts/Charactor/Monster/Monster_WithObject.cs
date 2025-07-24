/// <summary>
/// so를 통해서 추가적인 객체를 사용하는 몬스터이다.
/// </summary>
public class Monster_WithObject : BasicMonster
{
    protected new MonsterStat_WithObject_SO monsterSO { get { return base.monsterSO as MonsterStat_WithObject_SO; } }
}