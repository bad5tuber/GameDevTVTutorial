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
    [SerializeField] Transform parent;
    [SerializeField] int amountToIncrease = 5;

    [SerializeField] int hitPoints = 100;
    int currentScore;


    //this is saying that we're creating a variable called scoreboard from the ScoreBoard class
    ScoreBoard scoreBoard;

    void Start() 
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
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
        vfx.transform.parent = parent;
    }


    private void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        Destroy(this.gameObject);
    }
}
