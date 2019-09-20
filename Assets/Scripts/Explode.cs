using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public float particleLifetime = 10f;

    void Update()
    {
        
        //Replace "10f" with the particles lifetime
        Destroy(gameObject, particleLifetime + 0.1f);
    }
}
