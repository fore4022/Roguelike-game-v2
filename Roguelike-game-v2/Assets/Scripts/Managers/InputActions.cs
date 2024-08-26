using System.Collections.Generic;
using UnityEngine.InputSystem;
public static class InputActions
{
    public static List<IInputActionCollection> inputActionList = new List<IInputActionCollection>();

    public static void GetinputAction<T>(ref T inputAction) where T : IInputActionCollection
    {
        IInputActionCollection instance = inputActionList.Find(input => input.GetType() == typeof(T));
        bool isInclude = instance != null;

        if (isInclude)
        {
            inputAction = (T)instance;
        }
        else
        {
            T newInputAction;
        }
    }
    public static void EnableInputAction()
    {

    }
    public static void DisableInputAction()
    {

    }
}