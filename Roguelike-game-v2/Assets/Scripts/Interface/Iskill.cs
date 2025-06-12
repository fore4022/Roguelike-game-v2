using UnityEngine;
public interface ISkill
{
    public bool Finished { get; }
    public void Set();
    public void Enter(GameObject go);
}