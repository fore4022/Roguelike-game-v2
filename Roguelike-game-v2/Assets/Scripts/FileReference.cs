using System.Reflection;
[System.Serializable]
public class FileReference
{
    [ShowInInspector]
    public UnityEngine.Object targetObject;
    [ShowInInspector]
    public string[] fieldNames;
    [ShowInInspector]
    public string fieldName;

    public object GetValue()
    {
        if(targetObject ==  null || string.IsNullOrEmpty(fieldName))
        {
            return null;
        }

        var field = targetObject.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        return field?.GetValue(targetObject);
    }
    public void SetValue(object value)
    {
        if(targetObject == null || string.IsNullOrEmpty(fieldName))
        {
            return;
        }

        var field = targetObject.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        if(field != null && field.FieldType.IsAssignableFrom(value.GetType()))
        {
            field.SetValue(targetObject, value);
        }
    }
}