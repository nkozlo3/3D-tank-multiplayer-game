using UnityEngine;
using UnityEngine.AI;


public class Bots : MonoBehaviour
{
    [Tooltip("This is what our agent will react in relation to. AKA the player")]
    public GameObject target;

    [Tooltip("The patrol points")]
    public GameObject[] patrolPoints;

    [Tooltip("The distance from player")]
    public float distanceFromPlayer;

    [Tooltip("If the bot is pursuing player")]
    public static bool pursuing = false;

    // speed of this bot
    private float speed;

    //last position of this gameObject (used for calculating speed)
    Vector3 lastPosition;

    // The navmeshagent attached to this agent
    NavMeshAgent agent;

    //The spot in the array we are at
    int currentPatrolNumber = 0;

    /// <summary>
    /// Called at the first frame
    /// </summary>
    void Start()
    {
        // Getting NavMeshAgent component from this agent
        agent = GetComponent<NavMeshAgent>();

    }

    /// <summary>
    /// Called once per frame
    /// <see cref="Hide"/ called when bots health is too low>
    /// </summary>
    void Update()
    {
        Debug.Log(pursuing);

        // distance from player
        distanceFromPlayer = Vector3.Distance(gameObject.transform.position, target.transform.position);

        // if health is high enough and distance to player is far enough wander randomly
        if (EnemyHealth.currentHealth > 30f && distanceFromPlayer > 75f) { GoToNextPoint(); pursuing = false; }

        // Hide behind an obstacle if health gets too low
        if (EnemyHealth.currentHealth <= 30f) { Hide(); pursuing = false; }

        // if healt his high enough and target gets close enough pursue target
        if (EnemyHealth.currentHealth > 30f && distanceFromPlayer < 75f) { Pursue(); pursuing = true; }
    }

    /// <summary>
    /// Makes the agent hide behind an obstacle with the tag hide
    /// <see cref="Seek"/ the location this bot will travel towards when called upon>
    /// </summary>
    void Hide()
    {
        // Distance from obstacles
        float dist = Mathf.Infinity;

        // The vector this agent will travel towards (The vector behind the obstace)
        Vector3 chosenSpot = Vector3.zero;

        // For loop finds the length from each obstacle available to hide behind inside of World class
        for (int i = 0; i < World.Instance.GetHidingSpots().Length; i++) 
        {
            // Vector of current HidingSpot - vector of target to hide from
            Vector3 hideDir = World.Instance.GetHidingSpots()[i].transform.position - target.transform.position;

            // Vector of current hiding spot in for loop plus the hide direction, normalized (between 0 and 1) * 5
            Vector3 hidePos = World.Instance.GetHidingSpots()[i].transform.position + hideDir.normalized * 5f;

            // if distance from this bot to the current hide position is less than the distance to the hide position of i
            if (Vector3.Distance(this.transform.position, hidePos) < dist)
            {
                // change spot to hide behind
                chosenSpot = hidePos;

                // change the distance from this bot to chosenSpot
                dist = Vector3.Distance(this.transform.position, hidePos);
            }
        }

        // Seek chosenSpot
        Seek(chosenSpot);
    }

    /// <summary>
    /// Sends bot towards chosen location
    /// </summary>
    /// <param name="location">Location bot will travel towards</param>
    void Seek(Vector3 location)
    {
        // Sets destination for NavMeshAgent to follow
        agent.SetDestination(location);
    }

    /// <summary>
    /// Makes this bot patrol accross the points in <see cref="patrolPoints"/>
    /// </summary>
    void GoToNextPoint()
    {

        // current patrol point in patrolPoints array
        GameObject currentPatrolPoint = patrolPoints[currentPatrolNumber];

        //if have not reached current patrol point yet
        if (Vector3.Distance(gameObject.transform.position, currentPatrolPoint.transform.position) > 4f)
        {
            // Move agent towards patrol point
            Seek(currentPatrolPoint.transform.position);
        } else // if has reached current patrol point
        {

            // if at the end of the array
            if (currentPatrolNumber == patrolPoints.Length - 1)
            {
                // restart array (we are patrolling in a square)
                currentPatrolNumber = 0;
            }
            else // if not at the end of the array
            {
                // set up next patrol point
                currentPatrolNumber++;
            }
        }
    }

    /// <summary>
    /// Pursue target AKA the player character
    /// </summary>
    void Pursue()
    {

        //direction of target 
        Vector3 targetDirection = target.transform.position - gameObject.transform.position;

        // angle this bot is travelling at in relation to target
        float angle = Vector3.Angle(gameObject.transform.forward, gameObject.transform.TransformVector(targetDirection));

        // angle from bot to target
        float toTargetAngle = Vector3.Angle(gameObject.transform.forward, gameObject.transform.TransformVector(targetDirection));

        // follow if heading in the relative direction of target
        if ((toTargetAngle > 90 && angle < 20))
        {
            // vector the bot will seek
            Vector3 character = target.transform.position + target.transform.position.normalized * 34f;

            // Seek character
            Seek(character);
        } else
        {
            Seek(target.transform.position + target.transform.forward * (speed * 2));
        }
    }

    /// <summary>
    /// Called every 0.2 seconds
    /// </summary>
    private void FixedUpdate()
    {
        // last position of the bot
        lastPosition = transform.position;
        // current speed of the bot based on last position and current position * 100
        speed = Vector3.Distance(lastPosition, transform.position) * 100f;

    }
}
