using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    WeaponController WC;
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] arrowSprites;

    private Vector2 currentTarget;

    private bool hasCollided = false;

    public float arrowVelocity = 1f;
    public int arrowDamage = 1;
    //for ArrowDecay()
    private bool toggleDecay = false;
    private float alpha = 0;
    public float alphaDuration = 1f;
    public float decayDelayDuration = 1f;
    private float decayDelay = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        WC = WeaponController.instance;
        currentTarget = WC.target;
    }

    // Update is called once per frame
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
        }
        
        if (toggleDecay)
        {
            ArrowDecay();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //See if the arrow has collided with a wall or non-destroyable object
        //If so, activate ArrowDecay() and keep the arrow "embeded" into the wall/object, but keep the origional sprite
        if (collision.gameObject.tag == "Wall")
        {
            hasCollided = true;
            transform.Translate(Vector3.right * 0.15f, Space.Self);
            toggleDecay = true;
        }

        //See if arrow has hit an enemy
        //damaged enemy, make the enemy show it has been hit, then delete object
        if (collision.gameObject.tag == "Enemy")
        {
            
        }
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
