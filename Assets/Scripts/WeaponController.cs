using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    [SerializeField] GameObject player;
    public float angle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 posOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        Vector2 mouseOnScreen = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        transform.right = mouseOnScreen - posOnScreen;

        angle = Mathf.Rad2Deg * Mathf.Atan2(transform.right.y, transform.right.x);
    }
}
