using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent enemy;
    public Transform playerTarget;

    private void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        enemy.SetDestination(playerTarget.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // The enemy collided with the player; damage the player's health.
            HealthManager healthManager = collision.gameObject.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.TakeDamage(5); // Deal 1 unit of damage.
            }
        }
    }
}
