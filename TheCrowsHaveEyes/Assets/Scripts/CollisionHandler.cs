using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem collisionParticles;
    [SerializeField] float nextLevelDelay = 1.0f;
    [SerializeField] Camera cam;

    float shake = 0f;
    float shakeAmount = 0.7f;
    float decreaseFactor = 1f;
    bool isTransitioning = false;
    bool collisionDisabled = false;
    private void Start() {
        //collisionParticles.Stop();
    }
    

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(this.name + "--Collided with--" + col.gameObject.name);
    }

    void OnTriggerEnter(Collider col)
    {
        if(isTransitioning || collisionDisabled) {return;}
        
        Debug.Log($"{this.name} Triggered with {col.gameObject.name}");
        switch (col.gameObject.tag)
        {
        
        // add in a friendly coin or something

	    //case "Friendly":
	    //break;

	    default:
        StartCrashSequence();
        // Debug.Log(this.name + "--Collided with--" + col.gameObject.name);
        
	    break;
        }
    }

    void StartCrashSequence() 
    {
        isTransitioning = true;
        collisionParticles.Play();

        // as we've defined the variable collisionParticles as a method of type ParticleSystem
        // we dont use ParticleSystem.Play()
        GetComponent<PlayerControls>().enabled = false;
        GetComponent<BoxCollider>().isTrigger = false;
        GetComponent<Rigidbody>().useGravity = true;
        
        Invoke("ReloadLevel", nextLevelDelay);
    }


        void ReloadLevel()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }

}





 
