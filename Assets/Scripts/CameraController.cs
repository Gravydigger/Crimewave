using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;
    private Vector3 offest;
    private Vector3 zOffset = new Vector3(0, 0, -1);
    InputController IC;
    public Vector2 mousePos;
    public Vector2 playerPos;
    private float maxScreenPoint = 0.8f;
    private Vector2 velocity = Vector2.zero;

    private void Awake()
    {
        offest = transform.position - player.transform.position;
        transform.position = zOffset;
        
    }

    void Start()
    {
        IC = InputController.instance;
    }

    void LateUpdate()
    {
        playerPos = player.transform.position;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = (playerPos + mousePos) / 2f;
        transform.position += zOffset;
        //transform.position = playerPos + offest;
    }
}
