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

        EditorGUI.BeginProperty(position, label, property);

        float halfWidth = position.width / 2f;

        Rect objectRect = new Rect(position.x, position.y, halfWidth - 5, position.height);

        EditorGUI.PropertyField(objectRect, targetProp, GUIContent.none);

        if(targetProp.objectReferenceValue != null)
        {
            var type = EditorUtility.InstanceIDToObject(targetProp.objectReferenceValue.GetInstanceID()).GetType();
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if(fields.Count() > 0)
            {
                var fieldNamesProp = property.FindPropertyRelative("fieldNames");
                string[] options = fields.Select(o => o.Name).ToArray();
                Rect dropdownRect = new Rect(position.x + halfWidth + 5, position.y, halfWidth - 5, position.height);
                int currentIndex = 0;

                fieldNamesProp.arraySize = options.Count();

                for(int i = 0; i < options.Count(); i++)
                {
                    fieldNamesProp.GetArrayElementAtIndex(i).stringValue = options[i];
                }

                if(fieldNameProp.stringValue != "")
                {
                    currentIndex = System.Array.IndexOf(options, fieldNameProp.stringValue);
                }

                int selected = EditorGUI.Popup(dropdownRect, currentIndex, options);

                fieldNameProp.stringValue = options[selected];
            }
        }

        EditorGUI.EndProperty();
    }
}