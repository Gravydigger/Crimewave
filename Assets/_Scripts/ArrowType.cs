using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName ="ArrowType", menuName = "ArrowType")]
public class ArrowType : ScriptableObject
{
    public Sprite[] arrowSprites;
    public int arrowDamage = 2;
    public float arrowVelocity = 10;

    //[Tooltip("Special Features")]
    public ArrowDamageType arrowDamageType;

    public enum ArrowDamageType
    {
        normal,
        explosive,
        incendiary,
    }

    //Variables for type "Explosive"
    [HideInInspector] public float explosiveRadius = 3f;
    [HideInInspector] public int explosiveDamage = 1;

    //Variables for type "Poison"
    [HideInInspector] public float incendiaryDuration = 5f;
    [HideInInspector] public int incendiaryDamage = 1;
}
