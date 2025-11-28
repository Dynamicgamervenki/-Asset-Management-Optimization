using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CollisionManager))]
public class CollisionManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var collisionType = serializedObject.FindProperty("CollisionType");
        EditorGUILayout.PropertyField(collisionType);

        EditorGUILayout.PropertyField(serializedObject.FindProperty("collisionName"));

        var selectedType = (CollisionType)collisionType.enumValueIndex;

        if (selectedType == CollisionType.Trigger)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onTriggerEnter"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onTriggerExit"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onTriggerStay"));
        }
        else
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onCollisionEnter"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onCollisionExit"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onCollisionStay"));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
