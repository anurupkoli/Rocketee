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
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;
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
            if (!audioSources[0].isPlaying)
            {
                if(!engineParticles.isPlaying){
                    engineParticles.Play();
                }
                audioSources[1].Stop();
                audioSources[0].PlayOneShot(rocekteeThrust);
            }
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }
        else
        {
            engineParticles.Stop();
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
            leftThrustParticles.Stop();
            if(!rightThrustParticles.isPlaying){
                rightThrustParticles.Play();
            }
            transform.Rotate(Vector3.forward * rotationThrust * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rightThrustParticles.Stop();
            if(!leftThrustParticles.isPlaying){
                leftThrustParticles.Play();
            }
            transform.Rotate(-Vector3.forward * rotationThrust * Time.deltaTime);
        }
        else{
            leftThrustParticles.Stop();
            rightThrustParticles.Stop();
        }
        rb.freezeRotation = false;//unfreezing game's physics rotation
    }
}
