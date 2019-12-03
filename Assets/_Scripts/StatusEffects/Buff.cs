using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Buff", menuName = "Buff", order = 1)]
public class Buff : Status {

    public EntityBase.Attribute attribute;
    [Tooltip("The percentage that this should change.")]
    public float modifier;

    public override void ApplyStatus(EntityBase eb)
    {
        // safety check to make sure we have something in this attribute
        if (eb.attributes.ContainsKey(attribute) == false)
            eb.attributes[attribute] = 0;

        eb.attributes[attribute] += modifier;
    }

    public override void RemoveStatus(EntityBase eb)
    {
        eb.attributes[attribute] -= modifier;
    }

    public override void UpdateStatus(EntityBase eb) { }
}
