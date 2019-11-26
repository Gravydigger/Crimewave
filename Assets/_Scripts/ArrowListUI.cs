using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowListUI : MonoBehaviour
{
    public WeaponController weaponController;
    public Arrow[] arrows;
    public ArrowUI prefab;

    void Start()
    {
        //For each arrow find its arrow type and weapon controller, and show it in the UI
        foreach (Arrow arrow in arrows)
        {
            ArrowUI ui = Instantiate(prefab, transform);
            ui.SetArrow(arrow, weaponController);
        }
    }
}
