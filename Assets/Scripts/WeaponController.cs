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
    CharacterManager CM;
    GameManager GM;

    public AudioSource bowDrawn;
    public AudioSource bowFire;

    [SerializeField] Sprite[] bowSprites;

    float leftOffset;
    float rightOffset;
    private Vector3 offset;
    [HideInInspector] public Vector3 target;

    private float fireDelay = 0;
    public float fireDelayDuration = 2;

    private void Awake()
    {
        instance = this;
        offset = transform.position - player.transform.position;
        leftOffset = -offset.x;
        rightOffset = offset.x;
    }

    void Start()
    {
        IC = InputController.instance;
        CM = CharacterManager.instance;
        GM = GameManager.instance;
    }

    void Update()
    {
        if (!GM.gameOver && !GM.isGamePaused)
        {
            BowMovement();
            Fire();
        }

        if (CM.isPlayerDead == true)
            gameObject.SetActive(false);
    }

    private void BowMovement()
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
    }

    private void Fire()
    {
        //allows the bow to be shot
        if (Input.GetButton("Fire1") == true)
        {
            if (Input.GetButtonDown("Fire1"))
                bowDrawn.Play();

            spriteRenderer.sprite = bowSprites[1];

            //Prevents spam firing
            fireDelay += Time.deltaTime;
        }

        //If fireDelay is less than fireDelayDuration, don't fire an arrow
        if (Input.GetButtonUp("Fire1") == true && fireDelay < fireDelayDuration)
        {
            spriteRenderer.sprite = bowSprites[0];
            fireDelay = 0;
        }

        //If fireDelay is equal or greater than fireDelayDuration, fire an arrow
        if (fireDelay >= fireDelayDuration)
        {
            spriteRenderer.sprite = bowSprites[2];

            if (Input.GetButtonUp("Fire1") == true)
            {
                spriteRenderer.sprite = bowSprites[0];
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                target.z = 0f;
                bowFire.Play();
                Rigidbody2D ArrowInstance = Instantiate(arrow, transform.position + transform.right * 0.3f, transform.rotation) as Rigidbody2D;
                fireDelay = 0;
            }
        }
    }

}
