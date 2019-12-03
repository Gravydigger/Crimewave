using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;

[CreateAssetMenu(fileName = "DoT", menuName = "DoT", order = 1)]
public class DamageOverTime : Status {

    public int damagePerTick;
    [Tooltip("How often in seconds the damage gets applied (e.g. Take damage every X seconds).")]
    public float frequency;

    //Per-instance data
    [HideInInspector] public float nextTick = 0;

    //Does nothing
    public override void ApplyStatus(EntityBase eb) { }
    public override void RemoveStatus(EntityBase eb) { }

    //DoT's tick down every frame
    public override void UpdateStatus(EntityBase eb)
    {
        if (timer > nextTick)
        {
            eb.ApplyDamage(damagePerTick);
            nextTick += frequency;
        }
    }
}
