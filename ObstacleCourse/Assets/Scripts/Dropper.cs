using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{

    [SerializeField] float timeVar = 3.0f;
    MeshRenderer render;
    Rigidbody rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        // GetComponent<MeshRenderer>().enabled = false;
        // instead of using above, have cached method for both render and rigidBody
        render = GetComponent<MeshRenderer>();
        render.enabled = false;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeVar) 
        {
            Debug.Log(timeVar + " seconds have elapsed");
            render.enabled = true;
            rigidbody.useGravity = true;
        }
    }
}
