using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ToTheMoon/EnemyData", order = 4)]
public class EnemyData : ScriptableObject
{
    //public string enemyName;
    public GameObject enemyModel;
    public float maxHealth;
    public float currentHealth;
    [NonSerialized]
    public UnityEvent<float> addStatEvent;
    [NonSerialized]
    public UnityEvent<float> healthChangedEvent;
    // public float speed;
    // public int damage;
    
    private void OnEnable()
    {
        maxHealth = 50f;
        currentHealth = maxHealth;
    }
    
    public void AddEnemyStat(float healthAmount)
    {
        maxHealth += healthAmount;
        addStatEvent?.Invoke(maxHealth);
    }
    
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthChangedEvent?.Invoke(currentHealth);
    }
    
}
