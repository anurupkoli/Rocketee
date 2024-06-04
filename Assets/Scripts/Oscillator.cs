using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Vector3 movementVector;
    float movementVectorFactor; //values ranging from 0 - 1;
    [SerializeField] float period = 6f; //to controle speed of oscillations {time to complete one oscillation}

    Vector3 position;
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon){return;}
        movementVectorFactor = CalculateMovementFactor();
        Vector3 newPosition = movementVector * movementVectorFactor;
        transform.position = position + newPosition;
    }

    float CalculateMovementFactor(){
        float cycles = Time.time / period; //continuely growing w.r.t time
        float tau = Mathf.PI * 2;          // constant value of 6.283....
        float rawSinWave = Mathf.Sin(cycles * tau); //-1 to 1 values
        return (rawSinWave + 1f)/2f; //recalculate to 0 to 1 values
    }
}
