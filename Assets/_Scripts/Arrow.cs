using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    WeaponController WC;
    CharacterMovement CM;
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] arrowSprites;

    private Vector2 currentTarget;
    private Vector2 firedFrom;

    private bool hasCollided = false;

    public ArrowType arrowType;

    public float arrowVelocity = 1f;
    public int arrowDamage
    {
        get {
            // return arrowType != null ? arrowType.arrowDamage : 2;
            if (arrowType != null)
                return arrowType.arrowDamage;
            else
                return 2;
        }
    }



    //for ArrowDecay()
    private bool toggleDecay = false;
    private float alpha = 0;
    public float alphaDuration = 1f;
    public float decayDelayDuration = 1f;
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
        }

        //See if arrow has hit an enemy
        //If so, damaged the enemy and then delete gameObject
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyManager EM = targetRigidbody.GetComponent<EnemyManager>();
            EM.TakeDamage(arrowDamage, transform.position, firedFrom);
            Destroy(gameObject);
        }

        if (arrowType && arrowType.arrowDamageType == ArrowType.ArrowDamageType.explosive)
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
