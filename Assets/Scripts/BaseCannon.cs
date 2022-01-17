using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCannon : MonoBehaviour
{
    public GameObject bullet;
    public float bulletSpeed = 2f;
    bool cooldown = false;

    // Update is called once per frame
    void Update()
    {
       
       if (Input.GetKey(KeyCode.C) && cooldown == false && gameObject.tag == "Player")
        {
           StartCoroutine(Shoot());
        }

       if (Bots.pursuing == true && cooldown == false && gameObject.tag == "Enemy") { StartCoroutine(Shoot()); }

    }
    public IEnumerator Shoot()
    {
        cooldown = true;
        Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation);
        yield return new WaitForSeconds(1.1f);
        cooldown = false;
    }
}
