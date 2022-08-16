using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    //public static BossSpawner instance;
    public GameObject[] boss;
    public GameObject enemySpawner;
    

    private void Awake() 
    {
        
        PlayerPrefs.SetInt("BossLevel", 0);
        
    }

    private void Start() 
    {
        SpawnBoss();
    }
    
    
    public void SpawnBoss()
    {
        StartCoroutine(CountBeforeBoss(15));
        enemySpawner.SetActive(true);

    }

    IEnumerator CountBeforeBoss(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        int level = PlayerPrefs.GetInt("BossLevel");
        if (level >= boss.Length)
        {
            level -= 1;
        }
        Instantiate(boss[level], transform.position, Quaternion.identity);
        level += 1;
        PlayerPrefs.SetInt("BossLevel", level);
        enemySpawner.SetActive(false);
    }
}
