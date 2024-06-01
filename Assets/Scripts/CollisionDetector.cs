using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                NextLevel();
                break;
            default:
                CrashHandle();
                break;
        }
    }

    void CrashHandle(){
        GetComponent<Movement>().enabled = false;
        ReloadLevel();
    }
    void ReloadLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void NextLevel(){
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextLevel >= SceneManager.sceneCountInBuildSettings){
            nextLevel = 0;
        }
        SceneManager.LoadScene(nextLevel);
    }
}
