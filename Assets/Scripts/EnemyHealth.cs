using System.Collections;
using UnityEngine;

/// <summary>
/// Controls health of enemy
/// </summary>
public class EnemyHealth : MonoBehaviour
{

    [Tooltip("Starting health of enemy")]
    public float enemyHealth = 100f;

    public float playerHealth = 100f;

    // is tank already deactivated
    bool alreadyDead = false;

    // if player tank is already deactivated
    bool playerAlreadyDead = false;

    //amount of time until de-instantiation
    float deathTime = 1.5f;

    [Tooltip("Animation of death explosion")]
    public GameObject tankExplosion;

    [Tooltip("Current health of tank")]
    public static float currentHealth;

    [Tooltip("Current health of player tank")]
    public static float currentHealthPlayer;

    /// <summary>
    /// Called before the first frame update
    /// </summary>
    void Start()
    {
        //Sets currentHealth to be health
        if (gameObject.tag == "Enemy")
            currentHealth = enemyHealth;
        if (gameObject.tag == "Player")
            currentHealthPlayer = playerHealth;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        //if current health falls to zero and tank has not already died 
        if (currentHealth <= 0 && !(alreadyDead) && gameObject.tag == "Enemy")
        {
            // Start Die function
            Die();
        }

        if (currentHealthPlayer <= 0 && !(playerAlreadyDead) && gameObject.tag == "Player")
        {
            // Start Die function
            Die();
        }
    }

    /// <summary>
    /// Called when Something enters this objects collider
    /// </summary>
    /// <param name="collision">The collider entering this objects collider</param>
    public void OnTriggerEnter(Collider collision)
    {
        // if a bullet enters the collider of enemy
        if (collision.tag == "PlayerBullet" && gameObject.tag == "Enemy")
        {
            //Damage the tank based on BaseBullet's damage randomized 
            Damage(BaseBullet.rand);
        }

        // if bullet enters collider of player
        if (collision.tag == "EnemyBullet" && gameObject.tag == "Player")
        {
            //Damage the tank based on BaseBullet's damage randomized 
            DamagePlayer(BaseBullet.rand);
        }
    }

    /// <summary>
    /// Called if the tank takes damage, decreases tank health
    /// </summary>
    /// <param name="damage">The amount of damage gameObject takes</param>
    public void Damage(float damage)
    {
        // Decrease currentHealth by damage
        currentHealth -= damage;
        
        Debug.Log("Enemy health" + currentHealth);
    }

    /// <summary>
    /// Called if the tank takes damage, decreases tank health
    /// </summary>
    /// <param name="damage">The amount of damage gameObject takes</param>
    public void DamagePlayer(float damage)
    {
        // Decrease currentHealth by damage
        currentHealthPlayer -= damage;
        
        Debug.Log("Player health" + currentHealthPlayer);
    }

    /// <summary>
    /// Called when health falls bellow zero starts coroutine deathMechanics
    /// </summary>
    public void Die()
    {
        if (gameObject.tag == "Player")
        {
            playerAlreadyDead = true;
            StartCoroutine(DeathMechanics());
        }
        else if (gameObject.tag == "Enemy")
        {
            //Sets alreadyDead to true so gameObject cannot die again
            alreadyDead = true;
            //Starts couroutine DeathMechanics
            StartCoroutine(DeathMechanics());
        }
    }

    /// <summary>
    /// Destroys gameObject and starts tank explosion animation
    /// </summary>
    /// <returns>returns an amount of seconds to wait before destroying gameObject</returns>
    public IEnumerator DeathMechanics()
    {

        // Instantiate tankExplosion at position and rotation of gameObject
        Instantiate(tankExplosion, transform.position, Quaternion.identity);

        // wait for deathTime
        yield return new WaitForSeconds(deathTime);

        // Destroy this gameObject
        Destroy(gameObject);
    }
}
