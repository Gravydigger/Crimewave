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

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.T))
                Hit();
            if (Input.GetKeyDown(KeyCode.Y))
                Heal();
            if (Input.GetKeyDown(KeyCode.U))
                Slay();
        }
    }

    [ContextMenu("Player: Take 1 HP")]
    public void Hit()
    {
        CM.TakeDamage(1);
        Debug.Log("Player: Take 1 HP");
    }

    [ContextMenu("Player: Heal 1 HP")]
    public void Heal()
    {
        CM.HealHealth(1);
        Debug.Log("Player: Heal 1 HP");
    }

    [ContextMenu("Player: Slay Player")]
    public void Slay()
    {
        CM.currentHealth = 1;
        CM.TakeDamage(1);
        Debug.Log("Player: Slay Player");
    }
}
