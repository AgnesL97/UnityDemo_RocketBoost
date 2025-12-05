using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
   [SerializeField] float levelLoadDelay = 2f; 
   [SerializeField] AudioClip successSFX;
   [SerializeField] AudioClip crashSFX;
   [SerializeField] ParticleSystem successParticle;
   [SerializeField] ParticleSystem crashParticle;
   
   AudioSource audioSource; 
   
   bool isControllable = true; 
   
   void Start()
   {
      audioSource = GetComponent<AudioSource>();
   }
   void OnCollisionEnter(Collision other)
   {
      if(!isControllable) { return; }
        
      switch (other.gameObject.tag) 
      {
         case "Friendly":
            Debug.Log("This thing is friendly");
            break;
         case "Finish":
            StartSuccessSequence();
            break;
         default:
            StartCrashSequence();
            break;
      }
   }
   void StartSuccessSequence()
   {
      isControllable = false;
      audioSource.Stop();
      audioSource.PlayOneShot(successSFX);
      successParticle.Play();
      GetComponent<Movement>().enabled  = false;
      Invoke("LoadNextLevel", levelLoadDelay);
   }

   void StartCrashSequence()
   {
      isControllable = false;
      audioSource.Stop();
      audioSource.PlayOneShot(crashSFX);
      crashParticle.Play();
      GetComponent<Movement>().enabled  = false;
      Invoke("ReloadLevel", levelLoadDelay);
   }  


   void LoadNextLevel()
   {
      int currentScene = SceneManager.GetActiveScene().buildIndex;
      int nextScene = currentScene + 1;
      if (nextScene == SceneManager.sceneCountInBuildSettings)
      {
         nextScene = 0;
      }
      SceneManager.LoadScene(nextScene);
   }
   
   
   void ReloadLevel()
   {
      int currentScene = SceneManager.GetActiveScene().buildIndex;
      SceneManager.LoadScene(currentScene);
   }
} 

