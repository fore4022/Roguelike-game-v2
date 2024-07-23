using UnityEngine;
public abstract class BasicAttack : MonoBehaviour, IDamage
{
    public float Damage => throw new System.NotImplementedException();
    protected abstract void OnEnable();
    protected abstract void Init();
}
