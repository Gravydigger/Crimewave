using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;

[CreateAssetMenu(fileName = "DoT", menuName = "DoT", order = 1)]
public class DamageOverTime : Status {

    public int damagePerTick;
    [Tooltip("How often in seconds the damage gets applied (e.g. Take damage every 2 seconds).")]
    public float frequency;

    // per-instance data
    [HideInInspector] public float nextTick = 0;

    // do nothing
    public override void ApplyStatus(Health hp) { }
    public override void RemoveStatus(Health hp) { }

    // DoT's tick down every frame
    public override void UpdateStatus(Health hp)
    {
        if (timer > nextTick)
        {
            hp.ApplyDamage(damagePerTick);
            nextTick += frequency;
        }
    }
}
