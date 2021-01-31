using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Parameters - for tuning
    // Cache - e.g. refrences for readability or speed
    // State - private instance (member) variables

    // VARIABLES
    //
    // Parameters

    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float mainRotation = 100f;

    // Cache

    [SerializeField] ParticleSystem mainBoostParticles;
    [SerializeField] ParticleSystem leftBoostParticles;
    [SerializeField] ParticleSystem rightBoostParticles;
    [SerializeField] AudioClip mainEngine;

    // State

    Rigidbody rb;
    AudioSource audioSource;

    // START AND UPDATE METHODS

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    // PRIVATE GAME METHODS
    //
    // Thrusting
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
            StartThrusting();
        else
            StopThrusting();
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainBoostParticles.isPlaying)
        {
            mainBoostParticles.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainBoostParticles.Stop();
    }

    // Rotating

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            StartRotatingLeft();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            StartRotatingRight();
        }

        else
        {
            StopRotating();
        }
    }

    private void StartRotatingLeft()
    {
        ApplyRotation(mainRotation);
        if (!rightBoostParticles.isPlaying)
        {
            rightBoostParticles.Play();
        }
    }
    private void StartRotatingRight()
    {
        ApplyRotation(-mainRotation);
        if (!leftBoostParticles.isPlaying)
        {
            leftBoostParticles.Play();
        }
    }
    private void StopRotating()
    {
        rightBoostParticles.Stop();
        leftBoostParticles.Stop();
    }

    // We've taken an expansive set of code and extracted a method that is being passed
    // the parameter of rotationThisFrame. That is used in the codeblock and above, the
    // value being passed into the method parameter is our mainRotation variable declared
    // before the Start() method

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
