using UnityEditor;
[CustomEditor(typeof(Attack_SO))]
public class SO_Editor : Editor
{
    SerializedProperty show_1;
    SerializedProperty value_1;

    SerializedProperty show_2;
    SerializedProperty value_2;

    private void OnEnable()
    {
        show_1 = serializedObject.FindProperty("projectile");
        value_1 = serializedObject.FindProperty("projectile_Info");

        show_2 = serializedObject.FindProperty("isMultiCast");
        value_2 = serializedObject.FindProperty("multiCast");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawPropertiesExcluding(serializedObject, "projectile_Info", "multiCast");

        Show_1();
        Show_2();

        serializedObject.ApplyModifiedProperties();
    }
    private void Show_1()
    {
        if(show_1.boolValue)
        {
            EditorGUILayout.PropertyField(value_1);
        }
    }
    private void Show_2()
    {
        if(show_2.boolValue)
        {
            EditorGUILayout.PropertyField(value_2);
        }
    }
}