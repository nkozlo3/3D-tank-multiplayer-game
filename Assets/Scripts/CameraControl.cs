using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject playerCam;
    public GameObject hackedEnemeyCam;

    [Tooltip("Spiders Camera")]
    public GameObject spiderCam;

    [Tooltip("Drone camera")]
    public GameObject droneCam;

    /// <summary>
    /// Gives player control of enemy removes it from everything Else
    /// </summary>
    public void CameraSwitchEnemy()
    {
        
        //Deactivating player (if active)
        DeactivatePlayer();

        //Deactivating Spider Bot (if active)
        DeactivateSpider();

        //Deactivating drone
        DeactivateDrone();

        // Activating enemy for player
        //GivePlayerControlEnemy();
    }

    /// <summary>
    /// Gives player control of spider bot
    /// </summary>
    public void CameraSwitchSpider()
    {
        //Deactivating player (if active)
        DeactivatePlayer();

        //Deactivating enemy controls (if active)
        //DeactivateEnemyControls();

        //Deactivate drone controls
        DeactivateDrone();

        // Giving player control of spider
        GivePlayerControlSpider();
    }

    /// <summary>
    /// Gives player control of playerAgain
    /// </summary>
    public void CameraSwitchBackPlayer()
    {
        //Deactivating player (if active)
        DeactivateSpider();

        //Deactivating enemy controls (if active)
        //DeactivateEnemyControls();

        //Deactivating drone controls
        DeactivateDrone();

        // Giving player control of spider
        GivePlayerControlPlayer();
    }

    /// <summary>
    /// Gives Player control of Drone
    /// </summary>
    public void CameraSwitchDrone()
    {
        //Deactivating player (if active)
        DeactivateSpider();

        //Deactivating enemy controls (if active)
        //DeactivateEnemyControls();

        //Deactivating Player (if active)
        DeactivatePlayer();

        // Giving player control of spider
        GivePlayerControlDrone();
    }

    /// <summary>
    /// Deactivates players controls and camera
    /// </summary>
    private void DeactivatePlayer()
    {
        // turning off player camera
        playerCam.SetActive(false);

        // turning off player controls
        GameObject.Find("Tank").GetComponent<Movement>().enabled = false;
        GameObject.Find("Tank").GetComponent<HealthScript>().enabled = false;
    }

    /// <summary>
    /// Deactivating spider camera and scripts
    /// </summary>
    private void DeactivateSpider()
    {
        // turning off spider camera
        spiderCam.SetActive(false);

        //turning off spider controls
        GameObject.Find("Spider").GetComponent<Movement>().enabled = false;

        // turning off spider animations
        GameObject.Find("Spider").GetComponent<SpiderAnimationScript>().enabled = false;
    }

    /// <summary>
    /// Deactivating drone
    /// </summary>
   private void DeactivateDrone()
    {

        // Deactivating drone camera
        droneCam.SetActive(false);

        // Deactivating movement script 
        GameObject.Find("Drone").GetComponent<FlightMovement>().enabled = false;
    }

    /// <summary>
    /// Deactivating enemy control of tank
    /// </summary>
    //private void DeactivateEnemyControls()
    //{
    //    // Turning off enemy camera
    //    hackedEnemeyCam.SetActive(false);

    //    // Giving enemy AI controls back
    //    GameObject.Find("Enemy").GetComponent<EnemyNo1>().enabled = true;
    //    GameObject.Find("Enemy").GetComponent<EnemyHealth>().enabled = true;
    //    GameObject.Find("Enemy").GetComponent<Movement>().enabled = false;
    //    GameObject.Find("Enemy").GetComponent<HealthScript>().enabled = false;
    //}

    /// <summary>
    /// Giving player control of enemy
    /// </summary>
    //private void GivePlayerControlEnemy()
    //{
    //    // setting enemy camera active
    //    hackedEnemeyCam.SetActive(true);

    //    //Giving player control of enemy
    //    GameObject.Find("Enemy").GetComponent<EnemyNo1>().enabled = false;
    //    GameObject.Find("Enemy").GetComponent<EnemyHealth>().enabled = false;
    //    GameObject.Find("Enemy").GetComponent<Movement>().enabled = true;
    //    GameObject.Find("Enemy").GetComponent<HealthScript>().enabled = true;
    //}

    /// <summary>
    /// Gives player control of spider
    /// </summary>
    private void GivePlayerControlSpider()
    {
        // Turning on spider camera
        spiderCam.SetActive(true);

        // Turning on spider Movement
        GameObject.Find("Spider").GetComponent<Movement>().enabled = true;

        // Turning on spider animation
        GameObject.Find("Spider").GetComponent<SpiderAnimationScript>().enabled = true;
    }

    /// <summary>
    /// Gives player control of player
    /// </summary>
    private void GivePlayerControlPlayer()
    {
        // Activating player camera
        playerCam.SetActive(true);

        // activating player scripts
        GameObject.Find("Tank").GetComponent<Movement>().enabled = true;
        GameObject.Find("Tank").GetComponent<HealthScript>().enabled = true;
    }

    /// <summary>
    /// Giving player control drone
    /// </summary>
    private void GivePlayerControlDrone()
    {
        // activating drone camera
        droneCam.SetActive(true);

        // activating drone scripts
        GameObject.Find("Drone").GetComponent<FlightMovement>().enabled = true;
    }
}
