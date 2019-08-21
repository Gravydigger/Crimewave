﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject player;
    public static WeaponController instance;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D arrow;
    InputController IC;

    [SerializeField] Sprite[] sprites;

    float leftOffset;
    float rightOffset;
    private Vector3 offset;
    [HideInInspector] public Vector3 target;

    void Start()
    {
        instance = this;
        IC = InputController.instance;

        offset = transform.position - player.transform.position;
        leftOffset = -offset.x;
        rightOffset = offset.x;
    }

    void Update()
    {
        //Follows player
        transform.position = player.transform.position + offset;

        //Rotates to follow mouse
        Vector2 mousePos = Input.mousePosition;
        Vector2 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x -= objectPos.x;
        mousePos.y -= objectPos.y;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        //Sees where the mouse is, and moves the weapon in front or behind the player
        if (IC.mouseYCoord > 0)
        {
            spriteRenderer.sortingOrder = -1;
        }

        if (IC.mouseYCoord <= 0)
        {
            spriteRenderer.sortingOrder = 1;
        }

        //flips the weapon with the player
        if (IC.mouseXCoord < 0)
        {
            offset.x = leftOffset;
        }

        if (IC.mouseXCoord >= 0)
        {
            offset.x = rightOffset;
        }

        //allows the bow to be shot
        if (Input.GetButton("Fire1"))
        {
            spriteRenderer.sprite = sprites[2];
            //make it so bow cannot be spam fired
        }

        if (Input.GetButtonUp("Fire1"))
        {
            spriteRenderer.sprite = sprites[0];
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0f;
            Rigidbody2D ArrowInstance = Instantiate(arrow, transform.position, transform.rotation) as Rigidbody2D;
        }
    }
}
