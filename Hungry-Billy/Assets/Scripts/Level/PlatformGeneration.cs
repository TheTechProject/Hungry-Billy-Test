using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneration : MonoBehaviour
{
    [SerializeField] private float cannonSpawnHeight;
    [SerializeField] private GameObject cannonPrefab;
    // Detector for the cannons
    [SerializeField] private GameObject entryDetector;
    // Exit detector for the cannons
    [SerializeField] private GameObject exitDetector;
    // Positions for both detectors
    [SerializeField] private Transform entryDetectorPos;
    [SerializeField] private Transform exitDetectorPos;

    // Offset of the cannons
    private const float positionalOffset = -4.0f;

    // Physical Cannons
    private GameObject[] physCannons;

    // TO-DO: DO NOT EDIT THIS
    private int difficulty = 1;

    private int cannons;

    // Contraints for the procedural element of the cannons
    private Vector2 zSpawnLimits = new Vector2(-2.0f + positionalOffset, 2.0f + positionalOffset);
    private Vector2 xSpawnLimits = new Vector2(-6.0f + positionalOffset, 8.0f + positionalOffset);

    /// <summary>
    /// Chooses a random number of cannons to place on the platform
    /// based on the difficulty, then chooses random positions to place
    /// those cannons along the platform contraints.
    /// </summary>
    private void GenerateCannons()
    {
        cannons = Random.Range(1, 2) * difficulty;
        //poop
        physCannons = new GameObject[cannons];

        for(int i = 0; i < cannons; i++)
        {
            GameObject cannon;
            Vector3 cannonPosition;
            cannonPosition = new Vector3(Random.Range(transform.position.x + xSpawnLimits.x, transform.position.x + xSpawnLimits.y), 
                cannonSpawnHeight, 
                Random.Range(transform.position.z + zSpawnLimits.x, transform.position.y + zSpawnLimits.y));

            cannon = Instantiate(cannonPrefab, cannonPosition, Quaternion.identity);
            physCannons[i] = cannon;
        }
    }

    /// <summary>
    /// Generates the detectors for each side of the platform and
    /// sending over the cannons on the platform to have them start firing.
    /// </summary>
    private void GenerateDetectors()
    {
        GameObject en_Detect = Instantiate(entryDetector, entryDetectorPos.position, Quaternion.identity);
        RoverDetector en_Script = en_Detect.GetComponent<RoverDetector>();
        GameObject ex_Detect = Instantiate(exitDetector, exitDetectorPos.position, Quaternion.identity);
        RoverExitDetector ex_Script = ex_Detect.GetComponent<RoverExitDetector>();

        Enemy[] listOfEnemies = new Enemy[physCannons.Length];

        for(int i = 0; i < physCannons.Length; i++)
        {
            listOfEnemies[i] = physCannons[i].GetComponent<Enemy>();
        }

        en_Script.AddEnemyToList(listOfEnemies);
        ex_Script.AddEnemyToList(listOfEnemies);

    }

    public void GenerateLayout()
    {
        GenerateCannons();
        GenerateDetectors();
    }
}
