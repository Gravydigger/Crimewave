using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    WeaponController WC;
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] sprites;

    private Vector2 direction;
    private Vector2 currentTarget;
    private Vector2 currentPos;

    public float arrowVelocity = 1;
    //for ArrowDecay()
    private bool toggleDecay = false;
    private float alpha = 0;
    public float alphaDuration = 1;
    public float delayDuration = 1;
    private float delay = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        WC = WeaponController.instance;
        currentTarget = WC.target;
    }

    // Update is called once per frame
    void Update()
    {
        //Make arrow travel toward where the mouse was released
        transform.position = Vector2.MoveTowards(transform.position, currentTarget, arrowVelocity * Time.deltaTime);

        //See if the arrow has reached its destination, but hasn't collided with anything
        if (Vector3.Distance(transform.position, currentTarget) < 0.01f)
        {
            toggleDecay = true;
        }

        if (toggleDecay)
        {
            ArrowDecay();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //See if the arrow has collided with a wall or non-destroyable object
        //If so, activate ArrowDecay() and keep the arrow "embeded" into the wall/object, but keep the origional sprite 

        //See if arrow has hit an enemy
        //damaged enemy, make the enemy show it has been hit, then delete object
    }

    //Turns arrows into scenery, slowly makes then invisible, and then destroys the gameObject 
    void ArrowDecay()
    {
        //makes it appear to stick into the ground, and makes it unable to collide with anything
        spriteRenderer.sprite = sprites[1];
        Destroy(GetComponent<BoxCollider2D>());

        delay += Time.deltaTime;
        //sees if the "decay" delay has expired
        if (delay > delayDuration)
        {
            alpha += Time.deltaTime;
            spriteRenderer.color = Color.LerpUnclamped(Color.white, Color.clear, alpha / alphaDuration);
            Destroy(gameObject, alphaDuration + 0.01f);
        }
    }
}
