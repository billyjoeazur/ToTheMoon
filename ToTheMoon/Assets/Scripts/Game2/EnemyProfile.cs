using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EnemyProfile : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public Slider healthSlider;
    public SpawnEnemy spawn;
    public SpawnBoss spawnBoss;
    public GameManager gameManager;
    
    [NonSerialized]
    public UnityEvent<float> healthChangedEvent;
    
    private void Awake() 
    {
        spawnBoss = FindObjectOfType<SpawnBoss>();
        gameManager = FindObjectOfType<GameManager>();
    }
    
    private void Start()
    {
        currentHealth = maxHealth;
        healthSlider.GetComponent<HealthBar>().SetMaxHealth(maxHealth);
        healthSlider.GetComponent<HealthBar>().SetHealth(currentHealth);
    }
    
    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            DestroyEnemy();
        }
        healthChangedEvent?.Invoke(currentHealth);
        ChangeSliderValue(currentHealth);
    }
    
    public void ChangeSliderValue(float amount)
    {
        healthSlider.GetComponent<HealthBar>().SetHealth(amount);
    }
    
    public void DestroyEnemy()
    {
        if (this.gameObject.tag == "Boss")
        {
            spawnBoss.InstantiateBoss();
            spawnBoss.DropCoin(transform.position);
        }
        else
        {
            spawn.DropCoin(transform.position); // drop coin
        }
        int score = UnityEngine.Random.Range(4, 7);
        int expi = UnityEngine.Random.Range(1, 3);
        gameManager.AddScore(PlayerPrefs.GetInt("BossLevel") + score);
        gameManager.AddExpi(PlayerPrefs.GetInt("BossLevel") + expi);
        Destroy(gameObject); // destroy this enemy
    }
    
}
