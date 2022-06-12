using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBlocker : MonoBehaviour
{
    [SerializeField] GameObject player = null;
    FollowPlayer followPlayer;
    Van van;
    float lastZ;
    float rotAngle = 90f;
    
    // Start is called before the first frame update
    void Awake()
    {
        van = player.GetComponent<Van>();
        followPlayer = GetComponent<FollowPlayer>();
    }

    private void Start() 
    {
        lastZ = player.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        HandleBlockLogic();
    }

    private void HandleBlockLogic()
    {
        if (player.transform.position.z < lastZ || Mathf.Abs(player.transform.rotation.eulerAngles.y) > rotAngle)
        {
            followPlayer.enabled = false;
            return;
        }
        if (player.transform.position.z > lastZ || Vector3.Distance(transform.position, player.transform.position) > Mathf.Abs(followPlayer.GetOffset().z))
        {
            lastZ = player.transform.position.z;
            if (followPlayer.enabled == false)
            {
                followPlayer.enabled = true;
                return;
            }
        }
    }
}
