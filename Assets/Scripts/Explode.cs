using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    void Update()
    {
        //Replace "10f" with the particles lifetime
        Destroy(gameObject, 10f + 0.1f);
    }
}
