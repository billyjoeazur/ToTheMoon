using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public static Character instance;
    public PlayerSO playerSO;

    public float maxHealth;
    public float currentHealth;
    public HealthBar healthBar;

    public Text coinsText;
    public int currentCoin = 0;
    public Text diamondText;
    public int currentDiamond = 0;
    public Text scoreText;
    public Text expiText;
    
    public GameObject GameMenuPanel, newHighscore;
    public bool isNewHighscore = false;
    
    public GameObject[] spaceshipsObj;
    
    void Awake()
    {
        maxHealth = playerSO.player.maxHealth;
        
        PlayerPrefs.SetInt("CurrentScore", 0);
        PlayerPrefs.SetInt("CurrentXP", 0);
        Time.timeScale = 1f;
        // spaceshipsObj[playerSO.player.equipedSpaceship].SetActive(true);
        spaceshipsObj[0].SetActive(true);
        // PlayerPrefs.SetInt("CurrentSpaceship", playerSO.player.equipedSpaceship);
        PlayerPrefs.SetInt("CurrentSpaceship", 0);
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
        expiText.text = PlayerPrefs.GetInt("CurrentXP").ToString();
        
        //game over
        if(currentHealth <= 0)
        {
            Time.timeScale = 0f;
            GameMenuPanel.SetActive(true);
        }
        
        //check highscore
        if(PlayerPrefs.GetInt("CurrentScore") > playerSO.highestScore)
        {
            playerSO.highestScore = PlayerPrefs.GetInt("CurrentScore");
            newHighscore.SetActive(true);
            isNewHighscore = true;
        }
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
