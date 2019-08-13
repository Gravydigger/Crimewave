using System.Collections;
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
    public Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        IC = InputController.instance;

        offset = transform.position - player.transform.position;
        leftOffset = -offset.x;
        rightOffset = offset.x;
    }

    // Update is called once per frame
    void Update()
    {
        //Follows player
        transform.position = player.transform.position + offset;

        //Rotates to follow mouse
        Vector2 posOnScreen = Camera.main.WorldToViewportPoint(transform.position);
        Vector2 mouseOnScreen = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        transform.right = mouseOnScreen - posOnScreen;

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
        if (Input.GetButtonDown("Fire1"))
        {
            spriteRenderer.sprite = sprites[1];
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
