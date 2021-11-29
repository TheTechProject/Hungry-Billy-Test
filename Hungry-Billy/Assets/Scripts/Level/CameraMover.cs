using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    // So the camera moves only to the right
    private float movementDirection = -1.0f;

    [SerializeField] private float moveSpeed = 5.0f;

    // Camera starting position to line up with the track
    private Vector3 cameraStartPosition = new Vector3(0.0f, 6.0f, 9.0f);

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
    /// Moves the camera along the travel line.
    /// </summary>
    private void MoveCamera()
    {
        currentCameraPosition += movementDirection * Time.deltaTime * moveSpeed;
        transform.position = new Vector3(currentCameraPosition, transform.position.y, transform.position.z);
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
    }
}
