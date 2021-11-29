using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int health = 100;

    // The rover the turret is aiming for
    private GameObject currentTarget;

    // Used for firing animation
    private Animation animator;

    // Rovers that passed the entry detector for the current section
    List<GameObject> targetsInRange = new List<GameObject>();

    [SerializeField] private float attackCooldown = 2.0f;
    private float attackTimer; // Counts up to the attack Cooldown

    /// <summary>
    /// Taking damage from rover
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    /// <summary>
    /// Runs when a rover passes the entry detector. Adds them
    /// as a target to the turret.
    /// </summary>
    /// <param name="target"></param>
    public void AddNewTarget(GameObject target)
    {
        targetsInRange.Add(target);
        Debug.Log("Target Entered Area");
    }

    /// <summary>
    /// Runs when a rover passes the exit detector. Removes them as
    /// a target to the turret.
    /// </summary>
    /// <param name="target"></param>
    public void RemoveTarget(GameObject target)
    {
        targetsInRange.Remove(target);
        Debug.Log("Target Exited Area");
    }

    /// <summary>
    /// Attacks the current rover it is targeting after each cooldown interval.
    /// if the rover it's targeting is destroyed it will target another one
    /// in its sector.
    /// </summary>
    private void AttackTarget()
    {
        currentTarget = null;
        for(int i = 0; i < targetsInRange.Count; i++)
        {
            if(targetsInRange[i] != null)
            {
                currentTarget = targetsInRange[i];
                break;
            }
        }

        if(attackTimer >= attackCooldown && currentTarget != null)
        {
            //animator.Play();
            currentTarget.GetComponent<Rover>().Damage(25);
            attackTimer = 0.0f;
        }
    }    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0.0f)
        {
            Destroy(gameObject);
        }

        attackTimer += Time.deltaTime;
        if (targetsInRange.Count != 0)
        {
            AttackTarget();
        }
    }
}
