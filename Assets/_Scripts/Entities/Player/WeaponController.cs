﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponController : MonoBehaviour
{
    public GameObject player;
    public static WeaponController instance;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D[] arrowType;
    Rigidbody2D arrow;
    public int bowType = 0;

    InputController IC;
    CharacterManager CM;
    GameManager GM;

    public AudioSource bowDrawn, bowFire;

    [SerializeField] Sprite[] bowSprites;

    float leftOffset, rightOffset;
    private Vector3 offset;
    [HideInInspector] public Vector3 target;

    [HideInInspector] public float fireDelay = 0;
    public float fireDelayDuration = 2;

    private void Awake()
    {
        arrow = arrowType[0];
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

        if (CM.isDead)
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
            spriteRenderer.flipY = true;
        }

        if (IC.mouseXCoord >= 0)
        {
            offset.x = rightOffset;
            spriteRenderer.flipY = false;
        }
    }

    //public void SetArrow(int index)
    //{
    //    arrow = arrowType[index];
    //    bowType = index;
    //}

    public void SetArrow(Arrow arr)
    {
        arrow = arr.GetComponent<Rigidbody2D>();

        // find the matching index
        for (int i = 0; i < arrowType.Length; i++)
            if (arrowType[i] == arrow)
                bowType = i;
    }

    private void Fire()
    {
        //allows the bow to be shot
        if (Input.GetButton("Fire1") && EventSystem.current.IsPointerOverGameObject() == false)
        {
            if (Input.GetButtonDown("Fire1"))
                bowDrawn.Play();

            //Arrow nocked
            spriteRenderer.sprite = bowSprites[(bowType * 2) + 1];

            //Prevents spam firing
            fireDelay += Time.deltaTime;
        }

        //If fireDelay is less than fireDelayDuration, don't fire an arrow
        if (Input.GetButtonUp("Fire1") && fireDelay < fireDelayDuration)
        {
            spriteRenderer.sprite = bowSprites[0];
            fireDelay = 0;
        }

        //If fireDelay is equal or greater than fireDelayDuration, fire an arrow
        if (fireDelay >= fireDelayDuration)
        {
            //Arrow drawn
            spriteRenderer.sprite = bowSprites[(bowType * 2) + 2];

            if (Input.GetButtonUp("Fire1"))
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
