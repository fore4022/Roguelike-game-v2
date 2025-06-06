using UnityEditor;
using UnityEngine;
using System.Reflection;
[CustomPropertyDrawer(typeof(PlayerStat))]
public class PlayerStatDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect fieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

        EditorGUI.BeginProperty(position, label, property);

        var target = property.serializedObject.targetObject;
        var playerStatObj = fieldInfo.GetValue(target);

        if(playerStatObj == null)
        {
            return;
        }

        var fields = playerStatObj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        foreach(var field in fields)
        {
            if(field.IsDefined(typeof(PlayerStatAttribute), false))
            {
                object value = field.GetValue(playerStatObj);

                if(value is int intValue)
                {
                    int newVal = EditorGUI.IntSlider(fieldRect, field.Name, intValue, 0, 100);

                    if(newVal != intValue)
                    {
                        field.SetValue(playerStatObj, newVal);
                    }
                }
                else if(value is float floatValue)
                {
                    float newVal = EditorGUI.Slider(fieldRect, field.Name, floatValue, 0f, 10f);

                    if(newVal != floatValue)
                    {
                        field.SetValue(playerStatObj, newVal);
                    }
                }

                fieldRect.y += EditorGUIUtility.singleLineHeight + 2;
            }
        }

        EditorGUI.EndProperty();
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var playerStatObj = fieldInfo.GetValue(property.serializedObject.targetObject);
        int count = 0;

        if(playerStatObj != null)
        {
            var fields = playerStatObj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach(var field in fields)
            {
                if(field.IsDefined(typeof(PlayerStatAttribute), false))
                {
                    count++;
                }
            }
        }

        return count * (EditorGUIUtility.singleLineHeight + 2);
    }
}