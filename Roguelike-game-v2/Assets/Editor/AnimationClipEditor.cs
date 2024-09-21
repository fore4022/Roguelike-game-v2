using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Test_EditorCustomize))]
[CanEditMultipleObjects]
public class AnimationClipEditor : Editor
{
    private SerializedProperty animationClip;

    private const string propertyName = "animationClip";

    private void OnEnable()
    {
        animationClip = serializedObject.FindProperty(propertyName);

        AnimationClip clip = target as AnimationClip;

        clip.name = "a";

        serializedObject.ApplyModifiedProperties();
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUIStyle style = EditorStyles.helpBox;

        GUILayout.BeginVertical(style);
        EditorGUILayout.PropertyField(animationClip, true);

        if(GUILayout.Button("Set Animation Clip"))
        {
            Debug.Log("test");
        }

        GUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}