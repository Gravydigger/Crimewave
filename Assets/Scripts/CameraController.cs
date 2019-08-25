using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Vector3 offest;

    void Start()
    {
        offest = transform.position - player.transform.position;
    }

    void Update()
    {
        transform.position = player.transform.position + offest;
    }
}
