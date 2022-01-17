using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed = 10f;
    public float rotationSpeed = 100f;
    Rigidbody rb;
    Vector3 jump;
    public float jumpHeight = 3f;
    private float moveVertical;
    private float rotateHorizontal;
    private bool moving = false;
    private bool idleAlreadyPlaying;
    private bool drivingAlreadyPlaying;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0, 2f, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow))
        {

            moving = true;
        } else
        {
            moving = false;
        }
            if (moving == false && idleAlreadyPlaying == false && AudioManagerScript.isPlaying == false)
        {
            idleAlreadyPlaying = true;
            drivingAlreadyPlaying = false;
            FindObjectOfType<SoundEffectManager>().Stop("EngineDriving");
            FindObjectOfType<SoundEffectManager>().Play("EngineIdle");
        }
        if (moving == true && drivingAlreadyPlaying == false && AudioManagerScript.isPlaying == false)
        {
            idleAlreadyPlaying = false;
            drivingAlreadyPlaying = true;
            FindObjectOfType<SoundEffectManager>().Stop("EngineIdle");
            FindObjectOfType<SoundEffectManager>().Play("EngineDriving");
        }
        if (AudioManagerScript.isPlaying == true)
        {
            FindObjectOfType<SoundEffectManager>().Stop("EngineIdle");
            FindObjectOfType<SoundEffectManager>().Stop("EngineDriving");
        }
        moveVertical = Input.GetAxis("Vertical");
        rotateHorizontal = Input.GetAxis("Horizontal");


        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(jump * jumpHeight, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        Turn();
        Move();
    }

    void Turn()
    {
        // Determine the number of degrees to be turned based on the input, speed and time between frames.
        float turn = rotateHorizontal * rotationSpeed * Time.deltaTime;

        // Make this into a rotation in the y axis.
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        // Apply this rotation to the rigidbody's rotation.
        rb.MoveRotation(rb.rotation * turnRotation);
    }
    private void Move()
    {

        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        Vector3 movement = transform.forward * moveVertical * speed * Time.deltaTime;

        // Apply this movement to the rigidbody's position.
        rb.MovePosition(rb.position + movement);
    }
}
