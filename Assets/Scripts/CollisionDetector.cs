using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    [SerializeField] AudioClip rocketeeDeath;
    [SerializeField] AudioClip levelComplete;

    AudioSource audioSource;
    void Start(){
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision other)
    {
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
        if(!audioSource.isPlaying){
            audioSource.PlayOneShot(rocketeeDeath);
        }
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
        if(!audioSource.isPlaying){
            audioSource.PlayOneShot(levelComplete);
        }
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
