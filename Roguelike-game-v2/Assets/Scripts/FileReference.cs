using System.Reflection;
[System.Serializable]
public class FileReference
{
    [PlayerStat]
    public UnityEngine.Object targetObject;
    [PlayerStat]
    public string fieldName;

    public object GetValue()
    {
        if(targetObject ==  null || string.IsNullOrEmpty(fieldName))
        {
            return null;
        }

        var type = targetObject.GetType();
        var field = type.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        return field?.GetValue(targetObject);
    }
    public void SetValue(object value)
    {
        if(targetObject == null || string.IsNullOrEmpty(fieldName))
        {
            return;
        }

        var type = targetObject.GetType();
        var field = type.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        if(field != null && field.FieldType.IsAssignableFrom(value.GetType()))
        {
            field.SetValue(targetObject, value);
        }
    }
}