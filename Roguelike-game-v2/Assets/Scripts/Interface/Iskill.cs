using UnityEngine;
public interface Iskill
{
    public bool Finished { get; }
    public void SetAttack();
    public void SetCollider();
    public void Enter(GameObject go);
}