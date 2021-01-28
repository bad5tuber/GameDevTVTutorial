using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()

    {
        // accessing transform of this game object (becaause we've put this script on our player)
        // . operator -> contained within transform... Unity has a built in method called Translate
        // Translate needs where want to go on the axes
        // Jumps one to the right
        float xValue = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float zValue = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        transform.Translate(xValue,0,zValue);
    }
}
