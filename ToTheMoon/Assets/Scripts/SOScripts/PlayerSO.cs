using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "ToTheMoon/PlayerSO", order = 0)]
public class PlayerSO : ScriptableObject
{
    public Player player;
    public int highestScore;
    public int currentHealth = 100;
    [SerializeField] private int maxHealth = 100;
    
    [NonSerialized]
    public UnityEvent<int> healthChangedEvent;
    [NonSerialized]
    public UnityEvent<Player> OnPlayerDataUpdate;
    
    
    private void OnEnable() 
    {
        currentHealth = maxHealth;
        if (healthChangedEvent == null)
        {
            healthChangedEvent = new UnityEvent<int>();
        }
    }
    
    public void DecreaseHealth(int amount)
    {
        currentHealth -= amount;
        healthChangedEvent.Invoke(currentHealth);
    }
    
    public void PlayerDataUpdate(Player newData)
    {
        player = newData;
        OnPlayerDataUpdate?.Invoke(player);
        
    }
}
