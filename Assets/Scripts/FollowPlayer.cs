using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public void LateUpdate()
    {
        //Quaternion temp = Quaternion.AngleAxis(12, Vector3.right);
        transform.rotation = player.transform.rotation;
        transform.position = player.transform.position + 8f * transform.up - 18f * transform.forward;
    }
}
