using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverDetector : MonoBehaviour
{
    [SerializeField] private Enemy[] enemiesInRange;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Rover")
        {
            foreach(var enemy in enemiesInRange)
            {
                enemy.AddNewTarget(other.gameObject);
            }
        }
    }
}
