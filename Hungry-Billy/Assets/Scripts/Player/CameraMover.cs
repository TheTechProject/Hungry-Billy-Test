using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    // So the camera moves only to the right
    private float movementDirection = -1.0f;

    [SerializeField] private float moveSpeed = 5.0f;

    // Camera starting position to line up with the track
    [SerializeField] private Vector3 cameraStartPosition = new Vector3(0.0f, 6.0f, 9.0f);

    private List<Transform> roverPositions = new List<Transform>();

    // Position of the camera along the travel line
    private float currentCameraPosition = 0.0f;

    public float CurrentCameraPosition
    {
        get
        {
            return currentCameraPosition;
        }
    }

    /// <summary>
    /// Adds a rover to the list of Type Rover to be used
    /// for finding the rover in the lead of the track.
    /// </summary>
    /// <param name="newIndex"></param>
    public void AddNewRoverToList(Transform newIndex)
    {
        roverPositions.Add(newIndex);
    }

    private float positionOfFrontRover;

    /// <summary>
    /// Does a search to find out which rover is ahead of the other
    /// rovers. Then matches the camera position to it.
    /// </summary>
    private void FindPriority()
    {
        foreach(var position in roverPositions)
        {
            if (position != null && position.position.x < positionOfFrontRover)
            {
                positionOfFrontRover = position.position.x;
            }
        }
    }

    /// <summary>
    /// Moves the camera along the travel line.
    /// </summary>
    private void MoveCamera()
    {
        transform.position = new Vector3(positionOfFrontRover, transform.position.y, transform.position.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Move camera to start position
        transform.position = cameraStartPosition;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        FindPriority();
    }
}
