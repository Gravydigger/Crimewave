using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialKeyboard : MonoBehaviour
{
    public GameObject player;
    SpriteRenderer sprite;
    InputController IC;

    Vector3 offset;
    Color startingColor;
    float fadeDelay = 0;
    float fadeDelayDuration = 1f;
    float beginFadeDelay = 0.25f;
    bool beginFade = false;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        offset = transform.position - player.transform.position;
        startingColor = sprite.color;
    }

    void Start()
    {
        IC = InputController.instance;
    }

    void Update()
    {
        //Follows player
        transform.position = player.transform.position + offset;

        //when player moves, start to fade out
        if (IC.moveUp || IC.moveLeft || IC.moveDown || IC.moveRight)
            beginFade = true;

        if (beginFade)
        {
            fadeDelay += Time.deltaTime;

            if (fadeDelay >= beginFadeDelay)
            {
                //turn the tutorial invisable
                sprite.color = Color.LerpUnclamped(startingColor, Color.clear, (fadeDelay - beginFadeDelay) / fadeDelayDuration);

                if (fadeDelay > fadeDelayDuration + 0.01f)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
