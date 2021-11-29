using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverSpawner : MonoBehaviour
{
    [SerializeField] private GameObject rover;

    [SerializeField] private int roversToSpawn = 0;

    [SerializeField] private float spawnHeight = 5.0f;

    [SerializeField] private float spawnOffset = .0f;

    [SerializeField] private CameraMover cameraMoverClass;

    [SerializeField] private RoverCommunicator roverComms;

    private Vector2 SpawnLimits = new Vector2(-3.0f, 3.0f);

    private Rover[] rovers;

    private void Start()
    {
        SpawnRovers();
        rovers = new Rover[roversToSpawn];
    }

    /// <summary>
    /// Spawns the number of rovers given into the scene
    /// at random areas. Then sends the Rover class to the
    /// roverCommunications class.
    /// </summary>
    public void SpawnRovers()
    {
        Vector3 spawnZone;
        float spawnX;
        float spawnZ;

        for(int i = 0; i < roversToSpawn; i++)
        {
            spawnX = Random.Range(SpawnLimits.x, SpawnLimits.y);
            spawnZ = Random.Range(SpawnLimits.x, SpawnLimits.y);

            spawnZone = new Vector3(spawnX, spawnHeight, spawnZ + spawnOffset);

            GameObject newRover = Instantiate(rover, spawnZone, Quaternion.identity);

            cameraMoverClass.AddNewRoverToList(newRover.transform);
            //rovers[i] = newRover.GetComponent<Rover>();
        }
        //roverComms.AddRovers(rovers);
    }
}
