using UnityEngine;
public abstract class ExceptionMonster : MonoBehaviour
{
    protected PlayerStat_SO stat;

    public PlayerStat_SO Stat { get { return stat; } }
    protected abstract void OnCollisionEnter2D(Collision2D collision);
}
