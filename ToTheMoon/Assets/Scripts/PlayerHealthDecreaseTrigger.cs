using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthDecreaseTrigger : MonoBehaviour
{
    [SerializeField, Tooltip("How much the player's health decrease.")]
    private int healthDecreaseAmount = 10;
    [SerializeField]
    private PlayerSO characterSO;
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            print("tiggered!");
            characterSO.DecreaseHealth(healthDecreaseAmount);
        }
        
    }
}
