using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBoss : MonoBehaviour
{
    public EnemySO[] enemySO;
    public GameObject coinModel;
    public GameObject diamondModel;
    public GameObject spawnEnemy;
    
    private void Awake() 
    {
        PlayerPrefs.SetInt("BossLevel", 0);
    }
    
    void Start()
    {
        InstantiateBoss();
    }
    
    public void InstantiateBoss()
    {
        if (PlayerPrefs.GetInt("BossLevel") >= enemySO.Length)
        {
            PlayerPrefs.SetInt("BossLevel", PlayerPrefs.GetInt("BossLevel") - 1);
        }
        StartCoroutine(CountBeforeBoss(10f));
        spawnEnemy.SetActive(true);
    }

    IEnumerator CountBeforeBoss(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        
        CreateBossWithScript(enemySO[PlayerPrefs.GetInt("BossLevel")].enemyModel, typeof(EnemyProfile));
        spawnEnemy.SetActive(false);
    }
    
    void CreateBossWithScript(GameObject enemyModel, Type type)
    {
        GameObject go = (GameObject)Instantiate(enemyModel, transform.position, Quaternion.identity);
        go.AddComponent(type);
        go.GetComponent<EnemyProfile>().maxHealth = enemySO[PlayerPrefs.GetInt("BossLevel")].maxHealth;
        go.GetComponent<EnemyProfile>().healthSlider = go.transform.GetChild(0).GetChild(0).GetComponent<Slider>();
        int level = PlayerPrefs.GetInt("BossLevel");
        level++;
        PlayerPrefs.SetInt("BossLevel",  level);
    }
    
    public void DropCoinBoss(Vector3 transform)
    {
        int c = 0;
        while (c < enemySO[PlayerPrefs.GetInt("BossLevel") - 1].coinToDrop)
        {
            GameObject coin = Instantiate(coinModel, transform, Quaternion.identity);
            c++;
        }
    }
    
    public void DropCoin(Vector3 transform)
    {
        int c = 0;
        while (c < PlayerPrefs.GetInt("BossLevel") + 1)
        {
            GameObject coin = Instantiate(coinModel, transform, Quaternion.identity);
            c++;
        }
    }
}
