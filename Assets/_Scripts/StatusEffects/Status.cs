using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;

public abstract class Status : ScriptableObject {

    // how long this status effect lasts
    public float duration;

    // how long its been running currently (per-instance)
    public float timer;

    // TODO visual FX  that are played by this status
    // TODO icon - if we want icons on the HUD to represent them

    public abstract void ApplyStatus(IHealth ch);
    public abstract void UpdateStatus(IHealth ch);
    public abstract void RemoveStatus(IHealth ch);
}
