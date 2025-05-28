using UnityEngine;
public interface ISkill
{
    public bool Finished { get; }
    public void SetAttack();
    public void Enter(GameObject go);
}