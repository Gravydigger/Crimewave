using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ArrowType", menuName = "ArrowType")]
public class ArrowType : ScriptableObject
{
    public Sprite[] arrowSprites;
    public int arrowDamage = 2;
    public float arrowVelocity = 10;

    [Tooltip("special features")]
    public ArrowDamageType arrowDamageType;

    public enum ArrowDamageType
    {
        normal,
        explosive,
        incendiary,
    }

    //Variables for type "normal"
    //public ArrowDamageType damageTypeNormal = ArrowDamageType.normal;

    //Variables for type "Explosive"
    //public ArrowDamageType damageTypeExplosive = ArrowDamageType.explosive;
    public float explosiveRadius = 3f;
    public int explosiveDamage = 1;

    //Variables for type "Poison"
    //public ArrowDamageType damageTypeIncendiary = ArrowDamageType.incendiary;
    public float incendiaryDuration = 5f;
    public int incendiaryDamage = 1;

}
