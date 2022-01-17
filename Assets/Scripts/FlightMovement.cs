using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightMovement : MonoBehaviour
{
    [Tooltip("The rigid body of the drone")]
    Rigidbody rb;

    [Tooltip("The upforce of the drone")]
    public float upForce;

    [Tooltip("The rotation speed of drone controlled by horizontal keys")]
    public float rotationSpeedKeys = 2.5f;

    [Tooltip("The speed of the rotation")]
    public float rotationVelocity = 100f;
    // Forward speed
    private float movementForward = 500f;

    // tiltforward speed
    private float tiltForward = 0f;

    // tiltforward velocity
    private float tiltVelocity;


    // the current rotation of the drone
    private float currentRotation;

    // how much we want to rotate the drone
    private float futureRotation;
    /// <summary>
    /// Ran before scene loads
    /// </summary>
    void Awake()
    {
        // making rigid body Get this gameObjects RigidBody
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Called every 0.2 seconds
    /// </summary>
    void FixedUpdate()
    {
        // calling Movement functions
        MovementUpDown();
        MovementForward();
        RotateLeftRight();
        ClampSpeed();

        //Stopping the drone from getting under the map
        //if (rb.position.y <= 1f && !Input.GetKey(KeyCode.E))
        //{
            //rb.constraints = RigidbodyConstraints.FreezePositionX;
            //rb.velocity = Vector3.zero;
        //}
        // adding force vertically to move drone up or down
        //else
        //{
            rb.constraints = RigidbodyConstraints.None;
            // adding force in the up direction 
            rb.AddForce(Vector3.up * upForce);
        //}

        // changing rotation based on horizontal and vertical keys
        rb.rotation = Quaternion.Euler(new Vector3(tiltForward, currentRotation, rb.rotation.z));
    }


    /// <summary>
    /// Clamps the speed of the drone so it cannot infinitely speed up
    /// </summary>
    private void ClampSpeed()
    {
        // ifI am accelerating in any direction
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            // clamping the speed of the drone so it cannot speed up infinitely
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, Mathf.Lerp(rb.velocity.magnitude, 6f, Time.deltaTime));
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            // clamping the speed of the drone so it cannot speed up infinitely
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, Mathf.Lerp(rb.velocity.magnitude, 6f, Time.deltaTime));
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            // clamping the speed of the drone so it cannot speed up infinitely
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, Mathf.Lerp(rb.velocity.magnitude, 6f, Time.deltaTime));
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            // clamping the speed of the drone so it cannot speed up infinitely
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, Mathf.Lerp(rb.velocity.magnitude, 6f, Time.deltaTime));
        }
    }

    /// <summary>
    /// Rotating the drone left or right
    /// </summary>
    private void RotateLeftRight()
    {
        //making the rotation negative so it moves to the left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            futureRotation -= rotationSpeedKeys;
        }
        // making the rotation positive so it moves to the right
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            futureRotation += rotationSpeedKeys;
        }

        // changing the rotation see Fixed update
        currentRotation = Mathf.SmoothDamp(currentRotation, futureRotation, ref rotationVelocity, 0.25f);
    }

    /// <summary>
    /// Up down movement of drone
    /// </summary>
    private void MovementUpDown()
    {
        // adding force to Vector3.up
        if (Input.GetKey(KeyCode.E))
        {
            upForce = 450f;
        } else if (Input.GetKey(KeyCode.C)) // adding force to negative vector up (or vector down)
        {
            upForce = -200f;
        } else if (!Input.GetKey(KeyCode.C) && !Input.GetKey(KeyCode.E)) // making force roughly equal to gravity so drone appears to hover
        {
            upForce = 98.1f;
        }
    }

    /// <summary>
    /// Forward movement and forward / backward tilting
    /// <see cref="FixedUpdate"/>
    /// </summary>
    private void MovementForward()
    {
        // if up/down arrow keys or w/s are pressed
        if (Input.GetAxis("Vertical") != 0)
        {
            //adds force to the rigidBody of our drone 
            rb.AddRelativeForce(Vector3.forward * movementForward * Input.GetAxis("Vertical"));
            // will change rotation on the x axis in FixedUpdate
            tiltForward = Mathf.SmoothDamp(tiltForward, 20 * Input.GetAxis("Vertical"), ref tiltVelocity, 0.1f);
        }
    }
}
