public class Monster_G : Monster_WithObject
{
    private string skillKey;

    protected override void Init()
    {
        skillKey = monsterSO.extraObject.name;

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