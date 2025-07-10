using UnityEngine;
public class DI_Container
{
    public IDefaultImplementable Get(DependencyType type, MonoBehaviour mono)
    {
        IDefaultImplementable defaultImplementable = null;

        switch(type)
        {
            case DependencyType.Move:
                defaultImplementable = new DefaultMoveable(mono);
                break;
        }

        return defaultImplementable;
    }
}