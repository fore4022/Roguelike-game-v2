using UnityEngine;
public class Monster_G : Monster_WithObject
{
    [SerializeField, Range(1f, 100f)]
    private float SpawnChance;

    private string visualizerKey;

    protected override void OnEnable()
    {
        base.OnEnable();

        // position adjustment
    }
    protected override void Init()
    {
        visualizerKey = monsterSO.visualizer.name;

        base.Init();
    }
    protected override void SetPosition()
    {
        if(Random.Range(1, 101) <= SpawnChance)
        {
            Vector3 position = Calculate.GetRandomVector();
            GameObject go = Managers.Game.objectPool.ActiveObject(visualizerKey);
        }
        else
        {
            base.SetPosition();
        }
    }
}