/// <summary>
/// so�� ���ؼ� �߰����� ��ü�� ����ϴ�, �÷��̾ ���ؼ� �����̴� �⺻ �����̴�.
/// </summary>
public class BasicMonster_WithObject : BasicMonster
{
    protected new MonsterStat_WithObject_SO monsterSO { get { return base.monsterSO as MonsterStat_WithObject_SO; } }
}