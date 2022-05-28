using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;
    public PlayerData playerData;

    public float maxHealth;
    public float currentHealth;
    public HealthBar healthBar;

    public Text coinsText;
    int currentCoin = 0;
    public Text diamondText;
    int currentDiamond = 0;
    public Text scoreText;

    void Awake()
    {
        maxHealth = playerData._maxHealth;
        PlayerPrefs.SetInt("BossLevel", 0);
        PlayerPrefs.SetInt("CurrentScore", 0);
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        coinsText.text = currentCoin.ToString();
        diamondText.text = currentDiamond.ToString();
        scoreText.text = PlayerPrefs.GetInt("CurrentScore").ToString();
        healthBar.SetHealth(currentHealth);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Coin")
        {
            //set current coins
            currentCoin += 5;

            Destroy(other.gameObject);
        }
        else if(other.gameObject.tag == "Diamond")
        {
            //set current coins
            currentDiamond += 1;

            Destroy(other.gameObject);
        }
        else if(other.gameObject.tag == "Enemy")
        {
            currentHealth -= 10;
            Destroy(other.gameObject);
        }
        else if(other.gameObject.tag == "EnemyBullet")
        {
            currentHealth -= 20;
            Destroy(other.gameObject);
        }
        else if(other.gameObject.tag == "Boss")
        {
            currentHealth -= 30;
            //Destroy(other.gameObject);
        }
    }

}
