using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;

    [SerializeField] private CameraMover camera;

    [SerializeField] private GameObject[] platforms;

    [SerializeField] private int platformsVisibleOnEachSide = 2;

    // The intervals between the targets the camera must reach
    // to spawn platforms.
    [SerializeField] private float invtervalsBetweenMilestones = 20.0f;

    // The next position the camera must reach to spawn a new platform
    private float cameraMilestone;

    // The current position of the camera on the line
    private float cameraPosition;

    /// <summary>
    /// Creates a new platform when given the xPosition it
    /// needs to be spawned at.
    /// </summary>
    /// <param name="xPosition">xPosition of the new platform</param>
    private void CreateNewPlatform(float xPosition)
    {
        for(int i = 0; i < platforms.Length; i++)
        {
            if(platforms[i] == null)
            {
                GameObject newPlatform = Instantiate(platformPrefab, new Vector3(xPosition, 0.0f, 0.0f), Quaternion.identity);
                platforms[i] = newPlatform;
                return;
            }
        }

        ReplaceOldPlatform(xPosition);
    }

    /// <summary>
    /// Replaces an old platform by destroying it, re-sorting the array
    /// and then creating a new platform for the last iteration of the array.
    /// </summary>
    /// <param name="xPosition">xPosition of the new platform</param>
    private void ReplaceOldPlatform(float xPosition)
    {
        

        // Destroy the first platform
        Destroy(platforms[0]);
        for(int i = 0; i < platforms.Length; i++)
        {
            if(i < platforms.Length - 1)
            {
                // Re-sort the array
                platforms[i] = platforms[i + 1];
            }
            else
            {
                // Create a new platform
                GameObject newPlatform = Instantiate(platformPrefab, new Vector3(xPosition, 0.0f, 0.0f), Quaternion.identity);
                platforms[i] = newPlatform;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        platforms = new GameObject[(platformsVisibleOnEachSide * 2) + 1];

        /*
         * Sets the ends of each side of the
         * platform for creating the start
         * platforms.
         */
        float furthestLeftPos = 0.0f + (20.0f * platformsVisibleOnEachSide);
        float furthestRightPos = 0.0f - (20.0f * platformsVisibleOnEachSide);

        /*
         * Creates the first platforms in
         * the level.
         */
        for(float i = furthestLeftPos; i >= furthestRightPos; i -= 20.0f)
        {
            CreateNewPlatform(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        cameraPosition = camera.CurrentCameraPosition;

        /*
         * When the camera has moved a certain
         * distance a new platform will spawn
         * ahead of it.
         */
        if(cameraPosition <= cameraMilestone)
        {
            CreateNewPlatform(platforms[4].transform.position.x - invtervalsBetweenMilestones);
            cameraMilestone -= invtervalsBetweenMilestones;
        }
    }
}
