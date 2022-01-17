using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderCamera : MonoBehaviour
{
    [Tooltip("The SpiderBot this camera will follow")]
    public GameObject spider;

    /// <summary>
    /// Called every frame
    /// </summary>
    public void LateUpdate()
    {
        // setting rotation to follow the spider
        transform.rotation = spider.transform.rotation;

        // setting position to follow spider from a distance
        transform.position = spider.transform.position + transform.up * 2f - transform.forward * 6f;
    }
}
