using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;

[CreateAssetMenu(fileName = "DoT", menuName = "DoT", order = 1)]
public class DamageOverTime : Status {

    public int damagePerTick;
    public float frequency;

    // per-instance data
    public float nextTick = 0;

    // do nothing
    public override void ApplyStatus(IHealth ch) { }
    public override void RemoveStatus(IHealth ch) { }

    // DoT's tick down every frame
    public override void UpdateStatus(IHealth ch)
    {
        if (timer > nextTick)
        {
            ch.ApplyDamage(damagePerTick);
            nextTick += frequency;
        }
    }
}
