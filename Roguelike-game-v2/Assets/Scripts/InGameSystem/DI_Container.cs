using UnityEngine;
public class DI_Container
{
    public T Get<T>(Transform transform) where T : IDefaultImplementable, new()
    {
        T obj = new T();

        obj.Set(transform);

        return obj;
    }
}