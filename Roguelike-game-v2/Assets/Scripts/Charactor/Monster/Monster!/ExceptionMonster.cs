using UnityEngine;
public abstract class ExceptionMonster : MonoBehaviour
{
    protected Stat_SO stat;

    public Stat_SO Stat { get { return stat; } }
    protected abstract void OnCollisionEnter2D(Collision2D collision);
}
