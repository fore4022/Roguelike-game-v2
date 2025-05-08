using UnityEngine;
public interface ISkill
{
    public bool Finished { get; }
    public void SetAttack();
    public void SetCollider();
    public void Enter(GameObject go);
}