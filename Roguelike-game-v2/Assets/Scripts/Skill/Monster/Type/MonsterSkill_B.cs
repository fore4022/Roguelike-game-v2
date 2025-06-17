using System.Collections;
using UnityEngine;
public class MonsterSkill_B : MonsterSkillBase
{
    //private const Vector3 

    protected override void Enable()
    {
        StartCoroutine(Casting());
    }
    protected override void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }

        Managers.Game.objectPool.DisableObject(gameObject);
    }
    protected override void Init()
    {


        base.Init();
    }
    protected override void SetActive(bool isActive)
    {
        col.enabled = isActive;

        base.SetActive(isActive);
    }
    private void OnDisable()
    {
        if(isInit)
        {
            SetActive(false);
        }
    }
    private IEnumerator Casting()
    {
        SetActive(true);

        while(true)
        {
            //
            
            yield return null;
        }
    }
}