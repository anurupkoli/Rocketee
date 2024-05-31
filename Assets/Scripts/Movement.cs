using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 200f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        RocketeeThrust();
        RocketeeRotate();
    }

    void RocketeeThrust(){
        if(Input.GetKey(KeyCode.Space)){
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }
    }

    void RocketeeRotate(){
        rb.freezeRotation = true; //freezing game's physics rotation so that I can maually rotate rocketee
        if(Input.GetKey(KeyCode.A)){
            transform.Rotate(Vector3.forward * rotationThrust * Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.D)){
            transform.Rotate(-Vector3.forward * rotationThrust * Time.deltaTime);
        }
        rb.freezeRotation = false;//unfreezing game's physics rotation
    }
}
