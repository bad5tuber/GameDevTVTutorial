using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // This is how I inittially set it up, but if we are simply applying
    //this script across all enemy prefabs, there's no need

    // private void OnParticleCollision(GameObject other) 
    // {
    //     GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    //     foreach(GameObject enemy in enemies){
    //     Destroy(enemy);}
    //     Debug.Log($"{this.name} was destroyed by {other.gameObject.name}");
    // }
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int amountToIncrease = 10;
    [SerializeField] int hitPoints = 5;



    //this is saying that we're creating a variable called scoreboard from the ScoreBoard class
    ScoreBoard scoreBoard;
    Rigidbody rb;
    GameObject parentGameObject;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        AddRigidBody(); 
    }

    private void AddRigidBody()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit(other); 
        
        if(hitPoints == 0)
        {
            KillEnemy();
        }
    }

    private void ProcessHit(GameObject other)
    {
        scoreBoard.IncreaseScore(amountToIncrease);
        hitPoints -= amountToIncrease;
        Debug.Log(hitPoints);
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
    }


    private void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        Destroy(this.gameObject);
    }
    
}
