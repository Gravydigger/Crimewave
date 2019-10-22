using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMouse : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    InputController IC;
    WeaponController WC;
    public SpriteRenderer[] enemies;

    [SerializeField] Sprite[] mouseSprites;

    Vector3 offset;
    Color startingColor;
    bool fadeIn = false;
    bool changeSprite = false;
    bool deleteGameObject = false;
    float appearDelay = 0;
    float appearDelayDuration = 1;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startingColor = spriteRenderer.color;
        spriteRenderer.color = Color.clear;
        offset = new Vector3(0, 1.5f, 1);
    }

    void Start()
    {
        IC = InputController.instance;
        WC = WeaponController.instance;
    }

    void Update()
    {
        //Follows above the cursor
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;

        //Turns on if the player can see an enemy
        if (enemies[0].isVisible || enemies[1].isVisible)
            fadeIn = true;

        if (fadeIn && !deleteGameObject)
        {
            //Fade tutorial in
            if (spriteRenderer.color != startingColor)
            {
                appearDelay += Time.deltaTime;
                spriteRenderer.color = Color.Lerp(spriteRenderer.color, startingColor, appearDelay / appearDelayDuration);
            }

            //If the player can fire, change the sprite, and make it re-fade back in
            if (WC.fireDelay >= WC.fireDelayDuration && !changeSprite)
            {
                appearDelay = 0;
                spriteRenderer.color = Color.clear;
                spriteRenderer.sprite = mouseSprites[1];
                changeSprite = true;
            }

            //if an arrow is fired, delete tutorial
            if (WC.fireDelay >= WC.fireDelayDuration && Input.GetButtonUp("Fire1"))
            {
                appearDelay = 0;
                deleteGameObject = true;
            }
        }

        if (deleteGameObject)
        {
            //Turns tutorial invisable
            appearDelay += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, Color.clear, appearDelay / appearDelayDuration);

            if (appearDelay > appearDelayDuration + 0.01f)
                Destroy(gameObject);
        }

    }
}
