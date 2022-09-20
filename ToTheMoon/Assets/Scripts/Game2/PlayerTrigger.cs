using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField, Tooltip("How much the player's health decrease.")]
    private float healthDecreaseAmount = 10f;
    private float healthDecreaseAmount2 = 50f;
    
    [SerializeField]
    private GameManager gameManager;
    
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Enemy"))
        {
            gameManager.DecreaseHealth(healthDecreaseAmount);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("EnemyBullet"))
        {
            gameManager.DecreaseHealth(healthDecreaseAmount);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Boss"))
        {
            gameManager.DecreaseHealth(healthDecreaseAmount2);
        }
        else if (other.CompareTag("Coin"))
        {
            gameManager.AddCoin(1);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Diamond"))
        {
            gameManager.AddDiamond(1);
            Destroy(other.gameObject);
        }
    }
}
