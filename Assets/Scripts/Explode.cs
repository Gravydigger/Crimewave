using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public ParticleSystem particle;

    void Update()
    {
        Destroy(gameObject, particle.main.duration + 0.1f);
    }
}
