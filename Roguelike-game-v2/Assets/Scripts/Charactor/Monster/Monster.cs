using UnityEngine;
public class Monster : MonoBehaviour
{
    protected virtual void OnEnable()
    {
        SetPosition();
    }
    protected virtual void SetPosition()
    {
        //
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }
    }
    protected virtual void Die()
    {

    }
}