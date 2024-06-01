using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Collided with friendly");
                break;
            case "Finish":
                Debug.Log("End of the game");
                break;
            default:
                Debug.Log("Collided with Obsticle");
                break;
        }
    }
}
