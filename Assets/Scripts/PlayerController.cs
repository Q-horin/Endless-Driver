using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] SpawnManager spawnManager;
    [SerializeField] float speed = 20f;
    [SerializeField] float turnSpeed = 5f;
    float horizontalInput;
    float forwardInput;
    bool insideTrigger;
    int count = 0;

    void Update()
    {
        HandleVerticalMovement();
        HandleHorizontalRotation();
    }

    private void HandleVerticalMovement()
    {
        forwardInput = Input.GetAxis("Vertical");
        forwardInput = Mathf.Clamp01(forwardInput);
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
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
