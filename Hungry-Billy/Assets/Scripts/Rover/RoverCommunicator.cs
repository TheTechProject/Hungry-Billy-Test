using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverCommunicator : MonoBehaviour
{
    [SerializeField] private Rover[] roverModules;

    /// <summary>
    /// Add rovers to the array to be alerted when
    /// a cursor input is given.
    /// </summary>
    /// <param name="rovers"></param>
    public void AddRovers(Rover[] rovers)
    {
        roverModules = new Rover[rovers.Length];
        for(int i = 0; i < rovers.Length; i++)
        {
            roverModules[i] = rovers[i];
        }
    }

    /// <summary>
    /// Send an attack signal to all rovers on
    /// the scene.
    /// </summary>
    /// <param name="target">The enemy selected by the cursor</param>
    public void SendAttackSignal(GameObject target)
    {
        foreach(var rover in roverModules)
        {
            if(rover != null)
                rover.BeginAttack(target);
        }
    }
}
