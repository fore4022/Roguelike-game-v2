using System;
using System.Reflection;
using UnityEngine;
[Serializable]
public class FileReference
{
    [ShowInInspector]
    public UnityEngine.Object targetObject;
    [ShowInInspector]
    public string fieldName;

    [HideInInspector]
    public Action GetAction = null;
    [HideInInspector]
    public Action SetAction = null;

    public object GetValue()
    {
        if(targetObject ==  null || string.IsNullOrEmpty(fieldName))
        {
            return null;
        }

        var field = targetObject.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        GetAction?.Invoke();

        return field?.GetValue(targetObject);
    }
    public void SetValue(object value)
    {
        if(targetObject == null || string.IsNullOrEmpty(fieldName))
        {
            return;
        }

        var field = targetObject.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        if(field != null && CanConvert(value, field.GetValue(targetObject).GetType()))
        {
            field.SetValue(targetObject, value);
        }

        field.FieldType.IsAssignableFrom(value.GetType());
        SetAction?.Invoke();
    }
    private bool CanConvert(object value, Type targetType)
    {
        try
        {
            var converted = Convert.ChangeType(value, targetType);

            return true;
        }
        catch
        {
            return false;
        }
    }
}