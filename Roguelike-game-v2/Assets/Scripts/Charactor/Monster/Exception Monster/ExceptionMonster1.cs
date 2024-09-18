using UnityEngine;
public class ExceptionMonster1 : Monster
{
    protected override void SetPosition()
    {
        throw new System.NotImplementedException();
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
