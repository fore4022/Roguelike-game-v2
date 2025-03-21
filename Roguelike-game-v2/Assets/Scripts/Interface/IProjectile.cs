using System.Collections;
public interface IProjectile : IAttacker
{
    public IEnumerator Moving();
}