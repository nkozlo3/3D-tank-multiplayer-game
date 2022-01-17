using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public float health = 100f;
    public Slider slider;
    public Image fillImage;
    public Color fullHealthColor = Color.green;
    public Color zeroHealthColor = Color.red;
    bool alreadyDead = false;
    float deathTime = 3f;
    public GameObject tankExplosion;
    float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentHealth);
        if (currentHealth <= 0 && !(alreadyDead))
        {
            Die();
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "EnemyBullet")
        {
            Damage(BaseBullet.rand);
        }
    }
    public void Damage(float damage)
    {
        Debug.Log("PlayerHealth" + currentHealth);
        HealthBar();
        currentHealth -= damage;
    }
    public void Die()
    {
        alreadyDead = true;
        StartCoroutine(DeathMechanics());
    }
    public void HealthBar()
    {
        slider.value = currentHealth;
        fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, currentHealth / health);
    }

    public IEnumerator DeathMechanics()
    {
        Instantiate(tankExplosion, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }
}
