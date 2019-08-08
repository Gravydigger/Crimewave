using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public float playerSpeed = 1;
    public float speed;
    //private new Rigidbody2D rigidbody;
    CameraController cc;

    float moveHorizontal;
    float moveVertical;

    // Start is called before the first frame update
    void Start()
    {
        //rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0);
        //rigidbody.AddForce(playerSpeed * movement);
        transform.position += movement * playerSpeed * Time.fixedDeltaTime;

        speed = Mathf.Sqrt(Mathf.Abs(Input.GetAxisRaw("Horizontal") + Input.GetAxisRaw("Vertical")));
    }
}
