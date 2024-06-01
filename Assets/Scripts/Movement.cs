using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource[] audioSources;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 200f;
    [SerializeField] AudioClip rocekteeThrust;
    [SerializeField] AudioClip rocketeeEngine;
    [SerializeField] ParticleSystem engineParticles;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSources = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        RocketeeThrust();
        RocketeeRotate();
    }

    void RocketeeThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            engineParticles.Play();
            if (!audioSources[0].isPlaying)
            {
                audioSources[1].Stop();
                audioSources[0].PlayOneShot(rocekteeThrust);
            }
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }
        else
        {
            audioSources[0].Stop();
            if (!audioSources[1].isPlaying)
            {
                engineParticles.Stop();
                audioSources[1].PlayOneShot(rocketeeEngine);
            }
        }
    }

    void RocketeeRotate()
    {
        rb.freezeRotation = true; //freezing game's physics rotation so that I can maually rotate rocketee
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThrust * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThrust * Time.deltaTime);
        }
        rb.freezeRotation = false;//unfreezing game's physics rotation
    }
}
