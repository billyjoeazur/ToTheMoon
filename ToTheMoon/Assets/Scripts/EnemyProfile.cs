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
    public EnemySO[] enemy;
    [SerializeField] private Slider healthSlider;
    
    [NonSerialized]
    public UnityEvent<float> healthChangedEvent;
    
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
    
    int c = 0;
    public void DestroyEnemy()
    {
        
        while (c < enemy[0].coinToDrop)
        {
            GameObject coin = Instantiate(enemy[0].coinModel, transform.position, Quaternion.identity);
            c++;
        }
        Destroy(gameObject); // destroy this enemy
    }
}
