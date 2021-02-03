using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float timeBeforeDestroy = 2.0f;

    private void Start() {
        Destroy(this.gameObject, timeBeforeDestroy);
    }
}
