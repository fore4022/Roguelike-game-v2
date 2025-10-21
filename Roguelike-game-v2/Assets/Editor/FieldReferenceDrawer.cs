using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Linq;
/// <summary>
/// <para>
/// FileReference���� ������ ������Ʈ�� ��� ����ȭ ������ �ʵ带 �˾� �������� ���� ����
/// </para>
/// �������� ������Ʈ�θ� ����, ���� ������Ʈ�� �������� ����
/// </summary>
[CustomPropertyDrawer(typeof(FileReference))]
public class FieldReferenceDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)//
    {
        var componentProp = property.FindPropertyRelative("component");
        var fieldNameProp = property.FindPropertyRelative("fieldName");

        EditorGUI.BeginProperty(position, label, property);// property

        float halfWidth = position.width / 2f;

        Rect objectRect = new Rect(position.x, position.y, halfWidth - 5, position.height);
        EditorGUI.PropertyField(objectRect, componentProp, GUIContent.none);

        if(componentProp.objectReferenceValue != null)
        {
            var type = EditorUtility.InstanceIDToObject(componentProp.objectReferenceValue.GetInstanceID()).GetType();
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

                if(selected != -1)
                {
                    fieldNameProp.stringValue = options[selected];
                }
            }
        }

        EditorGUI.EndProperty();
    }
}