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
    //[HideInInspector]
    //public BossStats bossStats;

    void Start()
    {

    }

    void Update()
    {

        if (timeBtwSpawn <= 0)
        {
            int level = PlayerPrefs.GetInt("BossLevel");
            if (Time.timeScale == 1)
            {
                Instantiate(enemy[level], transform.position, Quaternion.identity);
            }
            
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

    private void FixedUpdate() 
    {
        
    }
}
