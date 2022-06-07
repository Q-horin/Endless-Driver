using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentRotation : MonoBehaviour
{
    [SerializeField] float yOffset = 0.5f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Time.deltaTime * yOffset, 0, 0);
    }
}
