using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverCommunicator : MonoBehaviour
{
    [SerializeField] private Rover[] roverModules;

    /// <summary>
    /// Send an attack signal to all rovers on
    /// the scene.
    /// </summary>
    /// <param name="target">The enemy selected by the cursor</param>
    public void SendAttackSignal(GameObject target)
    {
        foreach(var rover in roverModules)
        {
            rover.BeginAttack(target);
        }
    }
}
