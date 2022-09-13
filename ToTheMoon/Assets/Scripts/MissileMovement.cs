using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMovement : MonoBehaviour
{
    public float moveSpeed;
    public float minDistance;
    
    Transform target;
    Vector3 noTarget = new Vector3(0, 77, 99);
    Vector3 targetPosition;
    Vector3 currentPosition;
    Rigidbody2D rb;
    GameObject[] enemy, boss;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get all enemy in the scene
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        boss = GameObject.FindGameObjectsWithTag("Boss");
        
        // Priority target is boss before normal enemy
        if(boss.Length > 0)
        {
            enemy = boss;
            // Find the nearest enemy
            target = FindNearestEnemy();
            
            // Get the position of the target
            targetPosition = target.position;
            
            // Get the current position of the object
            currentPosition = transform.position;
            
            // Move the missile towards the enemy object
            MoveTowardsTarget();
        }
        else if(enemy.Length > 0)
        {
            
            // Find the nearest enemy
            target = FindNearestEnemy();
            
            // Get the position of the target
            targetPosition = target.position;
            
            // Get the current position of the object
            currentPosition = transform.position;
            
            // Move the missile towards the enemy object
            MoveTowardsTarget();
        }
        else
        {
            targetPosition = noTarget;
            
            // Get the current position of the object
            currentPosition = transform.position;
            
            // Move the missile towards the enemy object
            MoveTowardsTarget();
        }
        
        // destroy missile when it's off the screen
        if (transform.position.y >= 70 || transform.position.x <= -50 || transform.position.x >= 50)
        {
            Destroy(gameObject);
        }
        
    }

    // Find the nearest enemy
    Transform FindNearestEnemy()
    {

        // Set the nearest enemy to the first object in the array
        Transform nearestEnemy = enemy[0].transform;

        // Set the minimum distance to the distance between the current object and the first object in the array
        float minDistance = Vector3.Distance(transform.position, enemy[0].transform.position);

        // Loop through the array to find the closest object
        for (int i = 0; i < enemy.Length; i++)
        {
            // Get the distance between the current object and the object in the array
            float distance = Vector3.Distance(transform.position, enemy[i].transform.position);

            // If the distance is smaller than the minimum distance, set the minimum distance to the new distance
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy[i].transform;
            }
        }

        // Return the nearest enemy
        return nearestEnemy;
    }

    // Move the object towards the target enemy
    void MoveTowardsTarget()
    {
        // Calculate the direction to the target enemy
        Vector3 direction = targetPosition - currentPosition;

        // Normalize the direction
        direction.Normalize();

        // Calculate the velocity
        Vector3 velocity = direction * moveSpeed * Time.deltaTime;

        // Move the object
        rb.MovePosition(currentPosition + velocity);
    }
}