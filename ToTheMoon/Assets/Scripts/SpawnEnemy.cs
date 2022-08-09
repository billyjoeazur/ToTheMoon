using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnEnemy : MonoBehaviour
{
    private float timeBtwSpawn;
    public float startTimeBtwSpawn;
    public float decreaseTime;
    public float minimumTime = 4f;
    
    public EnemySO[] enemySO;
    public GameObject coinModel;
    public GameObject diamondModel;
    public int enemyLevel;
    
    void Start()
    {
        enemyLevel = 0;
        StartCoroutine(CountBeforeEnemy(startTimeBtwSpawn));
    }
    
    IEnumerator CountBeforeEnemy(float timeBtwSpawn)
    {
        yield return new WaitForSeconds(timeBtwSpawn);
        if (timeBtwSpawn > minimumTime)
        {
            timeBtwSpawn -= decreaseTime;
        }
        print(timeBtwSpawn.ToString());
        CreateObjectWithScript(enemySO[enemyLevel].enemyModel, typeof(EnemyProfile), enemyLevel);
        StartCoroutine(CountBeforeEnemy(timeBtwSpawn));
    }
    
    void CreateObjectWithScript(GameObject enemyModel, Type type, int level)
    {
        GameObject go = (GameObject)Instantiate(enemyModel, transform.position, Quaternion.identity);
        go.AddComponent(type);
        go.GetComponent<EnemyProfile>().maxHealth = enemySO[level].maxHealth;
        go.GetComponent<EnemyProfile>().healthSlider = go.transform.GetChild(2).GetChild(0).GetComponent<Slider>();
        go.GetComponent<EnemyProfile>().spawn = this.gameObject.GetComponent<SpawnEnemy>();
    }
    
    
    public void DropCoin(Vector3 transform)
    {
        int c = 0;
        while (c < enemySO[enemyLevel].coinToDrop)
        {
            GameObject coin = Instantiate(coinModel, transform, Quaternion.identity);
            c++;
        }
    }
}
