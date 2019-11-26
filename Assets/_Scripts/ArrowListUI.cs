using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class ArrowListUI : MonoBehaviour
{
    public WeaponController weaponController;
    public Arrow[] arrows;
    public ArrowUI prefab;
    float uiDelay = 0;
    public float uiDelayDuration = 5f;

    ArrowUI[] uiArray;
    int currentIndex = 0;

    void Start()
    {
        uiArray = new ArrowUI[arrows.Length];

        //For each arrow find its arrow type and weapon controller, and show it in the UI
        int index = 0;
        foreach (Arrow arrow in arrows)
        {
            uiArray[index] = Instantiate(prefab, transform);
            uiArray[index].SetArrow(arrow, weaponController);
            index++;
        }
    }

    private void Update()
    {
        //If the mouse is scrolling, move the arrow select
        if (Input.mouseScrollDelta.y > 0)
            currentIndex--;
        if (Input.mouseScrollDelta.y < 0)
            currentIndex++;

        //Makes sure that the player can't go over bounds of the array 
        currentIndex = Mathf.Clamp(currentIndex, 0, uiArray.Length - 1);

        //Sets the current index
        for (int i = 0; i < uiArray.Length; i++)
            uiArray[i].SetSelected(i == currentIndex);

        ScrollwheelMovement();
    }

    void ScrollwheelMovement()
    {
        //sees if the scroll wheel has been touched
        if (Input.mouseScrollDelta != Vector2.zero)
        {
            ShowUI(true);
            uiDelay = uiDelayDuration;
        }

        //if it as been touched, 
        if (uiDelay >= 0)
            uiDelay -= Time.deltaTime;

        if (uiDelay <= 0 || Input.GetButton("Fire1"))
            ShowUI(false);
    }

    public void ShowUI(bool visable)
    {
        if (visable)
            Tween.AnchoredPosition(transform as RectTransform, new Vector2(-50, 0), 0.5f, 0);
        else
            Tween.AnchoredPosition(transform as RectTransform, new Vector2(20, 0), 0.5f, 0);
    }
}
