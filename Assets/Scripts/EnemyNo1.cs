using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNo1 : MonoBehaviour
{
    public Transform player;
    public GameObject enemyBullet;
    public GameObject sphere;
    float distance;
    Rigidbody rb;
    float speed = 10f;
    public float health = 100f;
    bool alreadyShooting;
    //NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= 35f)
        {
            transform.position += Vector3.zero;
            if (!alreadyShooting)
            StartCoroutine(shoot());
            Turn();
        }
        if (distance <= 150f && distance >= 35f)
        {
            Turn();
            MoveTowardsPlayer();
        }
    }
    void Turn()
    {
        transform.LookAt(player);
    }
    void MoveTowardsPlayer()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Bullet")
        {
        }
    }
    IEnumerator shoot()
    {
        alreadyShooting = true;
        while (distance <= 150f)
        {
            yield return new WaitForSeconds(1.1f);
            Instantiate(enemyBullet, sphere.transform.position, Quaternion.identity);
        }
    }
}
