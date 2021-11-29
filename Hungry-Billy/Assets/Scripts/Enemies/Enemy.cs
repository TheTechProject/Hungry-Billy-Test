using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int health = 100;

    private GameObject currentTarget;

    List<GameObject> targetsInRange = new List<GameObject>();

    [SerializeField] private float attackCooldown = 2.0f;
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

    public void RemoveTarget(GameObject target)
    {
        targetsInRange.Remove(target);
        Debug.Log("Target Exited Area");
    }

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
            currentTarget.GetComponent<Rover>().Damage(25);
            attackTimer = 0.0f;
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
        if (targetsInRange.Count != 0)
        {
            AttackTarget();
        }
    }
}
