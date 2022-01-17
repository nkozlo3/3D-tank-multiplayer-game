using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class World
{
    private static readonly World instance = new World();
    private static GameObject[] hidingSpots;


    /// <summary>
    /// Constructor that finds all hidingSpots
    /// </summary>
    static World()
    {
        hidingSpots = GameObject.FindGameObjectsWithTag("hide");
    }

    private World() { }

    public static World Instance
    {
        get {
            return instance;
        }
    }


    /// <summary>
    /// Gets hiding spots / obstacles
    /// </summary>
    /// <returns> returns all hiding spots</returns>
    public GameObject[] GetHidingSpots()
    {
        return hidingSpots;
    }
}
