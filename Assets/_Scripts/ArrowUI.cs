using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowUI : MonoBehaviour
{
    public Arrow arrow;
    public WeaponController weaponController;
    public Image arrowImage;

    void Start()
    {
        //Automatically detects when the button is clicked, and calls the function OnClick()
        Button button = GetComponent<Button>();
        if (button != null)
            button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        //When button is clicked, use this type of arrow
        weaponController.SetArrow(arrow);
    }

    public void SetArrow(Arrow arr, WeaponController wc)
    {
        //Sets the type of arrows used, and the weapon controller. Called from ArrowListUI.cs
        arrow = arr;
        weaponController = wc;
        if (arrowImage)
            arrowImage.sprite = arrow.arrowSprites[0];
    }
}
