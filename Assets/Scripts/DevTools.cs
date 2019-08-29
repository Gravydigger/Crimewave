using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevTools : MonoBehaviour
{
    CharacterManager CM;
    WeaponController WC;
    InputController IC;

    // Start is called before the first frame update
    void Start()
    {
        CM = CharacterManager.instance;
        WC = WeaponController.instance;
        IC = InputController.instance;
    }

}
