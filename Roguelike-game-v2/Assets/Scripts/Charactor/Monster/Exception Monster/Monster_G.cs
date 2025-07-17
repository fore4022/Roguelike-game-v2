/// <summary>
/// 사망시, 현재 위치에 스킬을 시전한다.
/// </summary>
public class Monster_G : Monster_WithObject
{
    private string skillKey;

    protected override void Init()
    {
        skillKey = monsterSO.extraObjects[0].name;

        base.Init();
    }
    protected override void Die()
    {
        PoolingObject go = Managers.Game.objectPool.GetObject(skillKey);

        go.transform.position = transform.position;

        go.SetActive(true);

        base.Die();
    }
}