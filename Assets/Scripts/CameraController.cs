using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //[SerializeField] GameObject player; -- SerializeField means only the script can modify the gameobject player
    //public int number; -- and scprit can access it.

    [SerializeField] GameObject player;
    private Vector3 offest;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.Find("GAMEOBJECTNAME"); -- finds a gameobject in the editors hierarchy with the name GAMEOBJECTNAME, and lables it as the variable player
        offest = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offest;
    }
}
