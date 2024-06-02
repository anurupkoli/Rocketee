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
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void StartThrusting()
    {
        StartEngineThrustSound();
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    }

    void StopThrusting()
    {
        engineParticles.Stop();
        audioSources[0].Stop();
        StartEngineSound();
    }

    void StartEngineSound()
    {
        if (!audioSources[1].isPlaying)
        {
            engineParticles.Stop();
            audioSources[1].PlayOneShot(rocketeeEngine);
        }
    }

    void StartEngineThrustSound()
    {
        if (!audioSources[0].isPlaying)
        {
            StartEngineParticles();
            audioSources[1].Stop();
            audioSources[0].PlayOneShot(rocekteeThrust);
        }
    }

    void StartEngineParticles()
    {
        if (!engineParticles.isPlaying)
        {
            engineParticles.Play();
        }
    }

    void RocketeeRotate()
    {
        rb.freezeRotation = true; //freezing game's physics rotation so that I can maually rotate rocketee
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopThrustingParticles();
        }
        rb.freezeRotation = false;//unfreezing game's physics rotation
    }

    void RotateLeft()
    {
        FireRightThrustParticles();
        transform.Rotate(Vector3.forward * rotationThrust * Time.deltaTime);
    }

    void FireRightThrustParticles()
    {
        leftThrustParticles.Stop();
        if (!rightThrustParticles.isPlaying)
        {
            rightThrustParticles.Play();
        }
    }

    void RotateRight()
    {
        FireLeftThrustParticles();
        transform.Rotate(-Vector3.forward * rotationThrust * Time.deltaTime);
    }

    void FireLeftThrustParticles()
    {
        rightThrustParticles.Stop();
        if (!leftThrustParticles.isPlaying)
        {
            leftThrustParticles.Play();
        }
    }

    void StopThrustingParticles()
    {
        leftThrustParticles.Stop();
        rightThrustParticles.Stop();
    }
}
