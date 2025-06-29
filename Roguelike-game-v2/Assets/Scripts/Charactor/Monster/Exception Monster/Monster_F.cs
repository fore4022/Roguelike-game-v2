using UnityEngine;
public class Monster_F : Monster_WithObject
{
    [SerializeField, Min(0.1f)]
    private float splitScale = 0.5f;
    [SerializeField, Min(2)]
    private float splitInstanceCount = 2;

    private string visualizerKey;
    private float defaultScale;
    
    protected override void Init()
    {
        visualizerKey = monsterSO.extraObject.name;
        defaultScale = transform.localScale.x;

        base.Init();
    }
    protected override void Die()
    {
        if(transform.localScale.x != splitScale)
        {
            GameObject go;

            for(int i = 0; i < splitInstanceCount; i++)
            {
                go = Managers.Game.objectPool.ActiveObject(visualizerKey);

                go.transform.localScale = new Vector2(splitScale, splitScale);
                go.transform.position = transform.position;
            }
        }

        base.Die();
    }
}