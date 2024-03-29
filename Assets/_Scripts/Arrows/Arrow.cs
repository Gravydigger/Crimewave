﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    WeaponController WC;
    CharacterMovement CM;
    SpriteRenderer spriteRenderer;
    public Sprite[] arrowSprites;

    private Vector2 currentTarget;
    [HideInInspector] public Vector2 firedFrom;

    private bool hasCollided = false;

    //public ArrowType arrowType;

    public float arrowVelocity = 10f;

    public int arrowDamage = 2;

    public Status arrowStatus;

    //for ArrowDecay function
    private bool toggleDecay = false;
    private float alpha = 0;
    private float alphaDuration = 4f;
    private float decayDelayDuration = 1f;
    private float decayDelay = 0;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        WC = WeaponController.instance;
        CM = CharacterMovement.instance;
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), CM.GetComponent<BoxCollider2D>());

        currentTarget = WC.target;
        firedFrom = CM.playerPosition;

        if (transform.right.x < 0)
        {
            spriteRenderer.flipY = true;
        }
    }

    void Update()
    {
        //Make arrow travel toward where the mouse was released, provided it has not collided with anything
        if (!hasCollided)
            transform.position = Vector2.MoveTowards(transform.position, currentTarget, arrowVelocity * Time.deltaTime);

        //See if the arrow has reached its destination, but hasn't collided with anything
        if (Vector3.Distance(transform.position, currentTarget) < 0.01f)
        {
            //Makes it appear to stick into the ground
            spriteRenderer.sprite = arrowSprites[1];
            //Toggle decay
            toggleDecay = true;
            TriggerPayLoad();
        }
        
        if (toggleDecay)
        {
            ArrowDecay();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D targetRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

        //See if the arrow has collided with a wall or non-destroyable object
        //If so, activate ArrowDecay() and keep the arrow "embeded" into the wall/object, but keep the origional sprite
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "WallTop")
        {
            if (collision.gameObject.tag == "WallTop")
            {
                spriteRenderer.sprite = arrowSprites[1];
            }
            hasCollided = true;
            transform.Translate(Vector3.right * 0.2f, Space.Self);
            toggleDecay = true;

            TriggerPayLoad();
        }

        //See if arrow has hit an enemy
        //If so, damaged the enemy and then delete gameObject
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyManager EM = targetRigidbody.GetComponent<EnemyManager>();
            EM.TakeDamage(arrowDamage, transform.position, firedFrom);

            if (arrowStatus != null)
                EM.ApplyStatus(arrowStatus);

            TriggerPayLoad();
            Destroy(gameObject);
        }
    }

    void TriggerPayLoad()
    {
        //When the function is called, find scripts with the interface IPayLoad, and run the Deliver() function 
        IPayload[] payloads = GetComponents<IPayload>();
        foreach (IPayload payload in payloads)
            payload.Deliver();
    }

    //Turns arrows into scenery, slowly makes then invisible, and then destroys the gameObject 
    void ArrowDecay()
    {
        //Makes it unable to collide with anything
        Destroy(GetComponent<BoxCollider2D>());

        decayDelay += Time.deltaTime;
        //sees if the "decay" delay has expired
        if (decayDelay > decayDelayDuration)
        {
            alpha += Time.deltaTime;
            spriteRenderer.color = Color.LerpUnclamped(Color.white, Color.clear, alpha / alphaDuration);
            Destroy(gameObject, alphaDuration + 0.01f);
        }
    }
}
