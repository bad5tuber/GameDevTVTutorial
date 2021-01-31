using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float nextLevelDelay = 1.0f;
    [SerializeField] AudioClip collision;
    [SerializeField] AudioClip finishSound;
    [SerializeField] ParticleSystem collisionParticles;
    [SerializeField] ParticleSystem finishParticles;



    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() 
    {
        DebugController();
    }

    private void OnCollisionEnter(Collision other) 
    {
        // this is saying that if we're transitioning 
        // (which we are when our methods are being called),
        // do nothing
        if(isTransitioning || collisionDisabled) {return;}

        switch (other.gameObject.tag)
        {
	    case "Friendly":
	    break;

        case "Finish":
        StartSuccessSequence();
	    break;

	    default:
        StartCrashSequence();
        
	    break;
        }
    }

    void StartCrashSequence() 
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(collision);
        // as we've defined the variable collisionParticles as a method of type ParticleSystem
        // we dont use ParticleSystem.Play()
        collisionParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", nextLevelDelay);
    }
    void StartSuccessSequence ()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(finishSound);
        // as we've defined the variable finishParticles as a method of type ParticleSystem
        // we dont use ParticleSystem.Play()
        finishParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", nextLevelDelay);

    }
    void ReloadScene ()
    {
        StartCrashSequence();
        Invoke("ReloadLevel", nextLevelDelay);
    }


    void LoadNextLevel()
    {
        // at the same time as defining the active scene, we're also
        // going to save the next scene index in memory
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = ++activeSceneIndex;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
        nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    // create method to load scene called ReloadLevel
    // create a var of activeSceneIndex 
    void ReloadLevel()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }

    void DebugController()
    {
        if(Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKey(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; // allows us to toggle collision
        }
    }
}
