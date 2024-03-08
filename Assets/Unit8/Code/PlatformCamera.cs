using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCamera : MonoBehaviour
{
    public GameObject followTarget;
    public bool isFollowing;

    void LateUpdate()
    {
        if (isFollowing)
        {
            if (followTarget == null)
            {
                isFollowing = false;
            }
            else
            {
                Vector3 targetPos = followTarget.transform.position;
                transform.position = new Vector3(
                    targetPos.x, targetPos.y,
                     transform.position.z
                );
            }

        }
    }
}
