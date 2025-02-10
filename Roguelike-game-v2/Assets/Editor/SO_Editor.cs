using UnityEditor;
[CustomEditor(typeof(Attack_SO))]
public class SO_Editor : Editor
{
    SerializedProperty show;
    SerializedProperty value;

    private void OnEnable()
    {
        show = serializedObject.FindProperty("projectile");
        value = serializedObject.FindProperty("projectile_Info");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawPropertiesExcluding(serializedObject, "projectile_Info");

        if(show.boolValue)
        {
            EditorGUILayout.PropertyField(value);
        }

        serializedObject.ApplyModifiedProperties();
    }
}