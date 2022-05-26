using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemy;
    private float timeBtwSpawn;
    public float startTimeBtwSpawn;
    public float decreaseTime;
    public float minimumTime = 1f;

    void Start()
    {

    }

    void Update()
    {

        if (timeBtwSpawn <= 0)
        {
            int rand = Random.Range(0, enemy.Length);
            Instantiate(enemy[rand], transform.position, Quaternion.identity);
            timeBtwSpawn = startTimeBtwSpawn;
            if (startTimeBtwSpawn > minimumTime)
            {
                startTimeBtwSpawn -= decreaseTime;
            }
        }
        else
        {
            timeBtwSpawn -= decreaseTime;
        }
    }
}
