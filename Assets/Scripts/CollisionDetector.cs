using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField] float delay = 2f;
    [SerializeField] AudioClip rocketeeDeath;
    [SerializeField] AudioClip levelComplete;

    AudioSource[] audioSources;
    bool transitioning = false;
    void Start(){
        audioSources = GetComponents<AudioSource>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if(transitioning)
            return;

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Collided with friendly");
                break;
            case "Finish":
                LevelComplete();
                break;
            default:
                CrashHandle();
                break;
        }
    }

    void CrashHandle()
    {
        transitioning = true;
        audioSources[0].Stop();
        audioSources[0].PlayOneShot(rocketeeDeath);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delay);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LevelComplete()
    {  
        transitioning = true;
        audioSources[0].Stop();
        audioSources[0].PlayOneShot(levelComplete);
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", delay);
    }

    void NextLevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevel >= SceneManager.sceneCountInBuildSettings)
        {
            nextLevel = 0;
        }
        SceneManager.LoadScene(nextLevel);
    }
}
