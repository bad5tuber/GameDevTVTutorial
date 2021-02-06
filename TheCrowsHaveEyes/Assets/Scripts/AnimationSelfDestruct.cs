     using UnityEngine;
     using System.Collections;
     
     public class AnimationSelfDestruct : MonoBehaviour {
         public float delay = 15f;
     
         // Use this for initialization
         void Start () {
             Destroy (gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay); 
         }
     }