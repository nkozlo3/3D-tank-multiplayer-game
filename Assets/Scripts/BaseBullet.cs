using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BaseBullet : MonoBehaviour
{
    public float bulletSpeed;
    public GameObject player;
    public GameObject explosion;
    Rigidbody rb;
    public static float rand;

    Vector3 velocity;
    public SwitchMusicTrigger[] bulletSoundEffects;
    private void Awake()
    {

        player = GameObject.FindGameObjectWithTag("Player");

        rand = Random.Range(2f, 10f);
        rb = GetComponent<Rigidbody>();
        foreach (SwitchMusicTrigger s in bulletSoundEffects)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.pitch;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyBullet());
        velocity = gameObject.transform.forward * 47f;
        ShootingSound();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }
    public void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * .3f, ForceMode.Acceleration);
    }

    public IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(7f);
        Destroy(gameObject);
    }
    public void OnTriggerEnter(Collider other)
    {

        //play explosion sound
        FindObjectOfType<SoundEffectManager>().Play("BulletExplosion");

        //if other is not player and this is players bullet
        if (other.tag != "Player" && gameObject.tag == "PlayerBullet" && other.tag != "EnemyBullet")
        {
            // remove explosions parent
            explosion.transform.parent = null;
            // instantaite an explosion at gameObjects position and rotation
            Instantiate(explosion, transform.position, Quaternion.identity);
            // destroy bullet
            Destroy(gameObject);
        }
        // if other is not enemy and this is enemys bullet 
        if (other.tag != "Enemy" && gameObject.tag == "EnemyBullet" && other.tag != "PlayerBullet")
        {
            // remove explosions parent
            explosion.transform.parent = null;
            // instantaite an explosion at gameObjects position and rotation
            Instantiate(explosion, transform.position, Quaternion.identity);
            // destroy bullet
            Destroy(gameObject);
        }
    }
    void ShootingSound()
    {
        FindObjectOfType<SoundEffectManager>().Play("Shoot");
    }
}
