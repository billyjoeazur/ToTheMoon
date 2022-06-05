using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public static BossSpawner instance;
    public GameObject[] boss;
    int bossLevel = 0;
    public BossStats bossStats;

    private void Awake() 
    {
        bossStats.maxHealth = 0;
    }

    private void Start() 
    {
        SpawnBoss();
    }
    
    
    public void SpawnBoss()
    {
        StartCoroutine(CountBeforeBoss(4));

    }

    IEnumerator CountBeforeBoss(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //int rand = Random.Range(0, boss.Length);
        bossStats.maxHealth += 100;
        Instantiate(boss[bossLevel], transform.position, Quaternion.identity);
        bossLevel += 1;
    }
}
