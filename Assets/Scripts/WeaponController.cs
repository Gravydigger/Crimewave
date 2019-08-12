using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    [SerializeField] GameObject player;
    public static WeaponController instance;
    public SpriteRenderer spriteRenderer;
    InputController IC;

    public float mousePos;
    //public float angle;
    private Vector3 offest;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        IC = InputController.instance;

        offest = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Follows player
        transform.position = player.transform.position + offest;

        //Rotates to follow mouse
        Vector2 posOnScreen = Camera.main.WorldToViewportPoint(transform.position);
        Vector2 mouseOnScreen = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        transform.right = mouseOnScreen - posOnScreen;


        //angle = Mathf.Rad2Deg * Mathf.Atan2(transform.right.y, transform.right.x);

        mousePos = IC.mouseYCoord;

        if (IC.mouseYCoord > 0)
        {
            spriteRenderer.sortingOrder = -1;
        }

        if (IC.mouseYCoord <= 0)
        {
            spriteRenderer.sortingOrder = 1;
        }
    }
}
