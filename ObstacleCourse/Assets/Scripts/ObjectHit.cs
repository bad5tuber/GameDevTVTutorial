using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHit : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {

        // using the GetComponent method on the MeshRenderer and
        // selecting the material color, then changing it to magenta

        if (other.gameObject.tag == "Player")
        {
        GetComponent<MeshRenderer>().material.color = Color.white;
        gameObject.tag = "Hit";
        }

    }
}
