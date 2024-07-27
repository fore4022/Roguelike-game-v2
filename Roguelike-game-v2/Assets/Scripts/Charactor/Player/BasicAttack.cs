using UnityEngine;
public class BasicAttack : MonoBehaviour, IDamage
{
    private float damage;

    public float Damage => damage;
    private void Start()
    {
        damageCalculate();
    }
    private void damageCalculate()
    {
        damage = Managers.Game.player.Stat.damage;// attack type calculate;
    }
}