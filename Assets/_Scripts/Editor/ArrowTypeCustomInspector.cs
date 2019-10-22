using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(ArrowType))]
public class ArrowTypeCustomInspector : Editor
{
    /*public override void OnInspectorGUI()
    {
        ArrowType arrowType = (ArrowType)target;

        arrowType.arrowDamage = EditorGUILayout.IntField("Arrow Damage", arrowType.arrowDamage);
        arrowType.arrowVelocity = EditorGUILayout.FloatField("Arrow Velocity", arrowType.arrowVelocity);
        //arrowType.arrowSprites = EditorGUILayout;

        arrowType.damageTypeNormal = (ArrowType.ArrowDamageType)EditorGUILayout.EnumPopup("Damage Type", arrowType.damageTypeNormal);

        if (arrowType.damageTypeNormal == ArrowType.ArrowDamageType.explosive)
        {
            arrowType.explosiveRadius = EditorGUILayout.FloatField("Explosive Radius", arrowType.explosiveRadius);
            arrowType.explosiveDamage = EditorGUILayout.IntField("Explosive Damage", arrowType.explosiveDamage);
        }

        if (arrowType.damageTypeNormal == ArrowType.ArrowDamageType.incendiary)
        {
            arrowType.incendiaryDuration = EditorGUILayout.FloatField("Poison Duration", arrowType.incendiaryDuration);
            arrowType.incendiaryDamage = EditorGUILayout.IntField("Poison Damage", arrowType.incendiaryDamage);
        }*/
    
}
