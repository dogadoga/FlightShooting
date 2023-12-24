using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode_gun : MonoBehaviour
{
    AudioSource explosionSound;
    // Start is called before the first frame update
    void Start()
    {
        explosionSound = GetComponent<AudioSource>();
        ParticleSystem exp = GetComponent<ParticleSystem>();
        exp.Play();
        explosionSound.Play();
        Destroy(gameObject, exp.main.duration);
    }
}
