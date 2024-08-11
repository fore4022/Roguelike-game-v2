using UnityEngine;
public class BasicAttack : MonoBehaviour, IDamage
{
    public float Damage { get { return Managers.Game.player.basicAttack.Damage; } }

    private Collider2D col;

    private void Start()
    {
        col = GetComponent<Collider2D>();
    }
    private void Update()
    {
        Util.IsInvisible(col);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IDamageReceiver>(out IDamageReceiver damageReceiver))
        {
            damageReceiver.GetDamage(this);
        }
    }
}