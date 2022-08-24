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
    // public GameObject coinModel;
    // public GameObject diamondModel;
    
    private void Awake() 
    {
        //PlayerPrefs.SetInt("BossLevel", 0);
    }
    
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
        // go.GetComponent<EnemyProfile>().maxHealth = enemySO[PlayerPrefs.GetInt("BossLevel")].maxHealth;
        // go.GetComponent<EnemyProfile>().healthSlider = go.transform.GetChild(0).GetChild(0).GetComponent<Slider>();
        // go.GetComponent<EnemyProfile>().spawn = this.gameObject.GetComponent<SpawnEnemy>();
    }
    
    // public void DropCoin(Vector3 transform)
    // {
    //     int c = 0;
    //     while (c < enemySO[PlayerPrefs.GetInt("BossLevel")].coinToDrop)
    //     {
    //         GameObject coin = Instantiate(coinModel, transform, Quaternion.identity);
    //         c++;
    //     }
    // }
    
    
}
