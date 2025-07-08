using UnityEngine;
public class MonsterSkill_C : MonsterSkill_Base
{
    [SerializeField]
    private float slowDown = 0;

    protected override void Enable()
    {
        
    }
    protected override void Enter(GameObject go)
    {
        if(go.CompareTag("Player"))
        {
            //Managers.Game.player.
        }
    }
}