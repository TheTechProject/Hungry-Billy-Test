using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int health = 100;

    private GameObject currentTarget;

    List<GameObject> targetsInRange = new List<GameObject>();

    private float attackCooldown;
    private float attackTimer;

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public void AddNewTarget(GameObject target)
    {
        targetsInRange.Add(target);
        Debug.Log("Target Entered Area");
    }

    private void AttackTarget()
    {
        currentTarget = targetsInRange[0];

        if(attackTimer >= attackCooldown)
        {
            currentTarget.
        }
    }    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0.0f)
        {
            Destroy(gameObject);
        }

        attackTimer += Time.deltaTime;

        AttackTarget();
    }
}
