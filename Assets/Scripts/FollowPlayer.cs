using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject player = null;
    [SerializeField] Vector3 offset = new Vector3();
    Vector3 desiredPos;

    // Update is called once per frame
    void LateUpdate()
    {
        desiredPos = player.transform.position + offset;
        transform.position = desiredPos;
    }

    public Vector3 GetOffset()
    {
        return offset;
    }
}
