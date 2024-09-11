using System.Collections.Generic;
using UnityEngine.InputSystem;
public static class InputActions
{
    public static List<IInputActionCollection> inputActionList = new List<IInputActionCollection>();

    public static T GetInputAction<T>() where T : IInputActionCollection, new()
    {
        IInputActionCollection instance = inputActionList.Find(input => input.GetType() == typeof(T));
        bool isInclude = instance != null;

        if(inputActionList.Count == 0)
        {
            Managers.Scene.loadScene -= ClearActions;
            Managers.Scene.loadScene += ClearActions;
        }

        if (isInclude)
        {
            return (T)instance;
        }
        else
        {
            T newInputAction = new();

            inputActionList.Add(newInputAction);

            return newInputAction;
        }
    }
    public static void EnableInputAction<T>() where T : IInputActionCollection, new()
    {
        GetInputAction<T>().Enable();
    }
    public static void DisableInputAction<T>() where T : IInputActionCollection, new()
    {
        GetInputAction<T>().Disable();
    }
    public static void ClearActions()
    {
        inputActionList = new List<IInputActionCollection>();

        Managers.Scene.loadScene -= ClearActions;
    }
}