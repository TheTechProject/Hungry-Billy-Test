using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelector : MonoBehaviour
{
    [SerializeField] private RoverCommunicator roverCommunicator;

    Camera thisCamera;
    // Start is called before the first frame update
    void Start()
    {
        thisCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        Ray ray = thisCamera.ScreenPointToRay(Input.mousePosition);
       
        Debug.DrawRay(ray.origin, ray.direction * 100);

        if (Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(ray, out hit, 250))
            {
                GameObject hitGameObject = hit.collider.gameObject;
                if(hitGameObject.tag == "Enemy")
                {
                    roverCommunicator.SendAttackSignal(hitGameObject);
                }
            }
        }
    }
}
