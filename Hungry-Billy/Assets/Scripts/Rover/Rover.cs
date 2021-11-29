using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rover : MonoBehaviour
{
    // Configurable Rover Settings
    [SerializeField] private string name = "Rover Name";
    [SerializeField] private int health = 100;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float attackRange = 2.0f;
    [SerializeField] private float attackCooldown = 1.0f;

    // The base movement speed used for when speeds change
    private float originalSpeed;

    // Character Controller
    private CharacterController roverController;

    // Set to -1.0 to move right from the cameras perspective
    private const float moveDirection = -1.0f;

    // Set to true when a targets selected
    private bool attack = false;

    // Set to true when a target is in attack range
    private bool damagingTarget = false;

    // The current target the rovers attacking
    private GameObject currentAttackTarget;

    // Used as a cooldown when in attack range
    private float attackCooldownTimer = 0.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        roverController = gameObject.GetComponent<CharacterController>();

        originalSpeed = speed;
    }

    /// <summary>
    /// Constant forward movement for the Character controller.
    /// </summary>
    void MoveForward()
    {
        roverController.Move(new Vector3(moveDirection, 0.0f, 0.0f) * Time.deltaTime * speed);
    }

    /// <summary>
    /// Constant downward movement for the Character controller.
    /// </summary>
    void AddGravity()
    {
        roverController.Move(new Vector3(0.0f, -2.0f, 0.0f) * Time.deltaTime);
    }

    /// <summary>
    /// When a target is selected with the cursor the rovers
    /// will move towards it's Z position to be in line to attack
    /// it.
    /// </summary>
    void MoveToTarget()
    {
        if (transform.position.z >= currentAttackTarget.transform.position.z - 0.1f && transform.position.z <= currentAttackTarget.transform.position.z + 0.1f)
        {
            // Stay in current Z pos
            roverController.Move(new Vector3(0.0f, 0.0f, 0.0f) * Time.deltaTime * (speed * 2));
        }
        else if (currentAttackTarget.transform.position.z > transform.position.z)
        {
            // Move upwards from Z pos
            roverController.Move(new Vector3(0.0f, 0.0f, 1.0f) * Time.deltaTime * (speed * 2));
        }
        else if (currentAttackTarget.transform.position.z < transform.position.z)
        {
            // Move downward from Z pos
            roverController.Move(new Vector3(0.0f, 0.0f, -1.0f) * Time.deltaTime * (speed * 2));
        }
    }

    /// <summary>
    /// Called from the RoverCommunicator. Tells the rover
    /// to begin attacking a target.
    /// </summary>
    /// <param name="target">The target selected with the cursor</param>
    public void BeginAttack(GameObject target)
    {
        Debug.Log("Target Hit: Attacking");
        currentAttackTarget = target;
        attack = true;
    }

    /// <summary>
    /// Checks if the enemy is in range, and if it is
    /// it will damage the enemy every cooldown interval
    /// until the enemies health is at 0.
    /// </summary>
    private void AttackEnemy()
    {
        Vector3 roverCharacter = (transform.position + roverController.center);
        RaycastHit hit;

        if (Physics.SphereCast(roverCharacter, roverController.height, transform.forward, out hit, attackRange))
        {
            if(hit.collider.gameObject.tag == "Enemy")
            {
                currentAttackTarget = hit.collider.gameObject;
                speed = 0.0f;
                damagingTarget = true;
            }
        }

        if(damagingTarget == true && attackCooldownTimer >= attackCooldown)
        {
            if(currentAttackTarget != null)
            {
                currentAttackTarget.GetComponent<Enemy>().TakeDamage(25);
                attackCooldownTimer = 0.0f;
            }
            else
            {
                currentAttackTarget = null;
                damagingTarget = false;
                attack = false;
                speed = originalSpeed;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
        AddGravity();
        AttackEnemy();
        if (attack == true)
        {
            MoveToTarget();
        }
    }
}
