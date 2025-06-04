using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Linq;
[CustomPropertyDrawer(typeof(FileReference))]
public class FieldReferenceDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var targetProp = property.FindPropertyRelative("targetObject");
        var fieldNameProp = property.FindPropertyRelative("fieldName");

        Debug.Log(fieldNameProp);

        EditorGUI.BeginProperty(position, label, property);

        float halfWidth = position.width / 2f;

        Rect objectRect = new Rect(position.x, position.y, halfWidth - 5, position.height);

        EditorGUI.PropertyField(objectRect, targetProp, GUIContent.none);

        if(targetProp.objectReferenceValue != null)
        {
            var target = EditorUtility.InstanceIDToObject(targetProp.objectReferenceValue.GetInstanceID());
            var type = target.GetType();
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if(fields.Count() > 0)
            {
                string[] options = fields.Select(o => o.Name).ToArray();
                Rect dropdownRect = new Rect(position.x + halfWidth + 5, position.y, halfWidth - 5, position.height);
                int currentIndex = 0;

                if(fieldNameProp.stringValue != "")
                {
                    currentIndex = System.Array.IndexOf(options, fieldNameProp.stringValue);
                }

                int selected = EditorGUI.Popup(dropdownRect, currentIndex, options);

                Debug.Log(selected);

                fieldNameProp.stringValue = options[selected];
            }
        }

        EditorGUI.EndProperty();
    }
}