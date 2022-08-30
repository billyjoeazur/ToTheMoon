using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnEnemy : MonoBehaviour
{
    public float timeBtwSpawn;
    public float startTimeBtwSpawn;
    public float decreaseTime;
    public float minimumTime = 3f;
    
    public EnemySO[] enemySO;
    
    private void OnEnable() 
    {
        SpawnMonster();
    }
    
    public void SpawnMonster()
    {
        StartCoroutine(CountBeforeEnemy(startTimeBtwSpawn));
    }
    
    public IEnumerator CountBeforeEnemy(float timeBtwSpawn)
    {
        yield return new WaitForSeconds(timeBtwSpawn);
        if (timeBtwSpawn > minimumTime)
        {
            timeBtwSpawn -= decreaseTime;
        }
        if(this.gameObject.tag == "Boss")
        {
            CreateEnemyWithScript(enemySO[PlayerPrefs.GetInt("BossLevel") - 1].enemyModel, typeof(EnemyProfile));
            CreateEnemyWithScript(enemySO[PlayerPrefs.GetInt("BossLevel") - 1].enemyModel, typeof(EnemyProfile));
            CreateEnemyWithScript(enemySO[PlayerPrefs.GetInt("BossLevel") - 1].enemyModel, typeof(EnemyProfile));
        }
        else
        {
            CreateEnemyWithScript(enemySO[PlayerPrefs.GetInt("BossLevel")].enemyModel, typeof(EnemyProfile));
        }
        SpawnMonster();
    }
    
    public void CreateEnemyWithScript(GameObject enemyModel, Type type)
    {
        GameObject go = (GameObject)Instantiate(enemyModel, transform.position, Quaternion.identity);
        go.AddComponent(type);
    }
    
}
