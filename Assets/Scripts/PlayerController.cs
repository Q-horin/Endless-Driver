using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] SpawnManager spawnManager;
    [SerializeField] float maxVelocity = 20f;
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] float decelerationRate = 50f;
    [SerializeField] float accelerationRate = 10f;
    float initialVelocity = 0f;
    float currentVelocity;

    float horizontalInput;
    float forwardInput;
    bool insideTrigger;
    int count = 0;
    AudioPlayer audioPlayer;
    bool engineStarted;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Update()
    {
        HandleMainTheme();
        HandleHornEvent();
        HandleVerticalMovement();
        //HandleHorizontalRotation();
    }

    private void HandleMainTheme()
    {
        if (!engineStarted && (Input.GetAxis("Vertical") == 1))
        {
            audioPlayer.StartMusicAndSounds();
            engineStarted = true;
        }
    }

    private void HandleHornEvent()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            audioPlayer.StopClips();
            audioPlayer.PlayHornClip();
        }
    }

    private void HandleVerticalMovement()
    {
        if (!engineStarted && !audioPlayer.HasEnginSoundEnded()){return;}

        forwardInput = Input.GetAxis("Vertical");
        forwardInput = Mathf.Clamp01(forwardInput);
        if ( forwardInput == 1)
        {   
            currentVelocity = currentVelocity + (accelerationRate * Time.deltaTime);
            currentVelocity = Mathf.Clamp(currentVelocity, initialVelocity, maxVelocity);
            transform.Translate(Vector3.forward * currentVelocity * forwardInput);
        }
        else
        {
            currentVelocity = currentVelocity - (decelerationRate * Time.deltaTime);
            currentVelocity = Mathf.Clamp(currentVelocity, initialVelocity, maxVelocity);
            if(currentVelocity > 0) {
                transform.Translate(0, 0, currentVelocity);
            }
            else {
                transform.Translate(0, 0, 0);
            }
        }    
    }

    private void HandleHorizontalRotation()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("SpawnTrigger"))
        {
            if (!insideTrigger){ insideTrigger = true;}
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (insideTrigger && count < 1)
        {
        insideTrigger = false;
        StartCoroutine(SpawnRoad(other.transform.position));
        count++;
        }
    }

    IEnumerator SpawnRoad(Vector3 pos)
    {
        yield return new WaitUntil(() => Vector3.Distance(transform.position, pos) > 10f);
        spawnManager.SpawnTriggerEntered();
        count = 0;
    }
}
