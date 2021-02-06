using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    [Header("General Setup Settings")]

    [Tooltip("Defines how fast your chopper will be moving across the screen")]
    [SerializeField] [Range(0,50)] float movementSpeed = 30.0f;
    [Tooltip("Defines how fast your chopper can move on the X axis")]
    [SerializeField] float xRange = 12f;
    [Tooltip("Defines how fast your chopper can move on the Y axis")]
    [SerializeField] float yRange = 5f;


    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = -2f;

    [Header("Player input based tuning")]
    [SerializeField] float controlPitchFactor = 10f;
    [SerializeField] float controlRollFactor = 10f;
    [SerializeField] bool mouseInput = true;


    float controlPitch;
    float controlRoll;


    [Header("Laser Selection")]
    [Tooltip("Add all player lasers here")]
    
    // an array of type gameobject
    [SerializeField] GameObject[] lasers;


    float xThrow, yThrow, xYaw, yRoll, fireButton, xThrowMouse, yThrowMouse;


    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFire();
    }

    private void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        xThrowMouse = Input.GetAxis("Mouse X");
        yThrowMouse = Input.GetAxis("Mouse Y");

        if(mouseInput)
        {        
        float xOffset = xThrowMouse * Time.deltaTime * movementSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        
        float yOffset = yThrowMouse * Time.deltaTime * movementSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
        }
        else
        {
        float xOffset = xThrow * Time.deltaTime * movementSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * movementSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
        }        
    }

    void ProcessRotation()
    {
        if(mouseInput)
        {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrowMouse * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrowMouse * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
        }
        else
        {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
        }
    }

    void ProcessFire()
    {
        // if pushing fire button
        // print shooting
        // else dont print shooting

        if(Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    private void SetLasersActive(bool isActive)
    {
        foreach(GameObject laser in lasers) 
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}