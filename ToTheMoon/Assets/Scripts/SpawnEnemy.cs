using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private EnemySO[] enemySO;
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
            int level = PlayerPrefs.GetInt("BossLevel");
            if (Time.timeScale == 1)
            {
                Instantiate(enemySO[level].enemyModel, transform.position, Quaternion.identity);
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
}
