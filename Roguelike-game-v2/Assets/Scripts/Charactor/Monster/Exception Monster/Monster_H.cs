public class Monster_H : Monster_WithObject
{
    private string skillKey;

    protected override void Init()
    {
        skillKey = monsterSO.extraObject.name;

        base.Init();
    }
}