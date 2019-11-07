using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(ArrowType))]
public class ArrowTypeCustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        //Draws the fields in the inspector as it would normally
        ArrowType arrowType = (ArrowType)target;

        serializedObject.Update();

        DrawDefaultInspector();

        //If the arrow type is explosive, show related property fields
        if (arrowType.arrowDamageType == ArrowType.ArrowDamageType.explosive)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("explosiveDamage"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("explosiveRadius"));
        }

        //If the arrow type is explosive, show related property fields
        if (arrowType.arrowDamageType == ArrowType.ArrowDamageType.incendiary)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("incendiaryDuration"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("incendiaryDamage"));
        }
        serializedObject.ApplyModifiedProperties();
    }
}
