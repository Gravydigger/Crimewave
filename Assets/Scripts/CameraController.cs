using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;
    InputController IC;

    Vector3 mousePos, playerPos, refVel;

    float zOffset;
    //How far away the camara should be when the mouse is at the edge of the screen
    float cameraDistance = 3.5f;
    //how quickly the camera will move to its target position
    float smoothTime = 0.15f;


    private void Awake()
    {
        playerPos = player.position;
        zOffset = transform.position.z;
    }

    void Start()
    {
        IC = InputController.instance;
    }

    void FixedUpdate()
    {
        mousePos = CaptureMousePos();
        playerPos = UpdatePlayerPos();
        UpdateCameraPos();
    }

    Vector3 CaptureMousePos()
    {
        //find the mouse position on the screen
        Vector2 tempMousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //sets the camera's offset to the centre of the screen
        tempMousePos *= 2;
        tempMousePos -= Vector2.one;

        //Makes the distance from the edge of the screen consistant (optional)
        float max = 0.9f;
        if (Mathf.Abs(tempMousePos.x) > max || Mathf.Abs(tempMousePos.y) > max)
        {
            tempMousePos = tempMousePos.normalized;
        }

        return tempMousePos;
    }

    Vector3 UpdatePlayerPos()
    {
        Vector3 mouseOffset = mousePos * cameraDistance;
        Vector3 tempPlayerPos = player.position + mouseOffset;

        //prevents the camera being placed on the player, which would make the player area go out of view
        tempPlayerPos.z = zOffset;

        return tempPlayerPos;
    }

    void UpdateCameraPos()
    {
        Vector3 cameraPos = Vector3.SmoothDamp(transform.position, playerPos, ref refVel, smoothTime);
        transform.position = cameraPos;
    }
}
