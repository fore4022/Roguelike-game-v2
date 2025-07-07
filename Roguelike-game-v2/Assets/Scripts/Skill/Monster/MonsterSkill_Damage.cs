using System;
public class MonsterSkill_Damage : MonsterSkill_Base, IDamage
{
    private Func<float> damage = null;

    public Func<float> Damage { get { return damage; } set { damage = value; } }
    public float DamageAmount { get { return damage.Invoke(); } }
    protected override void Enable() { }
}