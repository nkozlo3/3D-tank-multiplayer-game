using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimationScript : MonoBehaviour
{
    [Tooltip("Animator controller of spider")]
    Animator anim;

    /// <summary>
    /// Called at the first frame
    /// </summary>
    void Start()
    {
        // Getting the animator component of this object
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update()
    {
        // Movement speed of spider
        float move = Input.GetAxis("Vertical");

        // Telling the animator how fast character is moving
        anim.SetFloat("Speed", move);
    }
}
