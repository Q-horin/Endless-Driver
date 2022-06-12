using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] SpawnManager spawnManager;
    Van van;
    bool insideTrigger;
    int count = 0;
    AudioPlayer audioPlayer;
    bool mainThemeHasStarted;
    bool hasBeenExited;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        van = GetComponent<Van>();
    }

    void Update()
    {
        if (!van.CanMove() && audioPlayer.HasEnginSoundEnded())
        {
            van.SetCanMove(true);
        }
        HandleMainTheme();
        HandleHornEvent();
    }

    private void HandleMainTheme()
    {
        if (!mainThemeHasStarted && van.IsEngineStarted())
        {
            audioPlayer.StartMusicAndSounds();
            mainThemeHasStarted = true;
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

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("SpawnTrigger"))
        {
            if (!insideTrigger){ insideTrigger = true;}
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (insideTrigger && count < 1 && !hasBeenExited)
        {
        insideTrigger = false;
        Debug.Log("yupi");
        StartCoroutine(SpawnRoad(other.transform.position));
        count++;
        hasBeenExited = true;
        }
    }

    IEnumerator SpawnRoad(Vector3 pos)
    {
        yield return new WaitUntil(() => Vector3.Distance(transform.position, pos) > 25f);
        Debug.Log("Actual shit running");
        hasBeenExited = false;
        spawnManager.SpawnTriggerEntered();
        count = 0;
    }
}
