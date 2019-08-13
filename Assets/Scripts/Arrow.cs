using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    WeaponController WC;

    private Vector2 direction;
    private Vector2 target;
    public float arrowVelocity = 1;
    private Vector2 currentPos;
    // Start is called before the first frame update
    void Start()
    {
        WC = WeaponController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, WC.target, arrowVelocity * Time.deltaTime);

        //See if the arrow has reached its destination
        if (transform.position == WC.target)
        {
            Destroy(gameObject);
        }
    }
}
