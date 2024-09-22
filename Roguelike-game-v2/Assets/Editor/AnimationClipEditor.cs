using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(SetAnimationClip))]
[CanEditMultipleObjects]
public class AnimationClipEditor : Editor
{
    private SerializedProperty animationClip;

    private const string propertyName = "animationClip";

    private void OnEnable()
    {
        animationClip = serializedObject.FindProperty(propertyName);

        AnimationClip clip = target as AnimationClip;

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
            Debug.Log(animationClip);
        }

        GUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}