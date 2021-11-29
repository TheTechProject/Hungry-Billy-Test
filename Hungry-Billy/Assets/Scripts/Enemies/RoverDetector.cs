using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverDetector : MonoBehaviour
{
    [SerializeField] private Enemy[] enemiesInRange;

    /// <summary>
    /// Adds the cannons on the platform of the detector
    /// for automating firing.
    /// </summary>
    /// <param name="enemies"></param>
    public void AddEnemyToList(Enemy[] enemies)
    {
        enemiesInRange = new Enemy[enemies.Length];
        int count = 0;
        foreach (var enemy in enemies)
        {
            enemiesInRange[count] = enemy;
            count++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /*
         * When a rover enters the detectors trigger it
         * will add them as targets to the cannon.
         */
        if (other.gameObject.tag == "Rover")
        {
            foreach(var enemy in enemiesInRange)
            {
                enemy.AddNewTarget(other.gameObject);
            }
        }
    }
}
