using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : MonoBehaviour
{
    public BossStats instance;

    int c = 0;
    [SerializeField]
    Coin coin;
    [SerializeField]
    Coin diamond;
    [SerializeField]
    BulletShower bulletShower;

    public float maxHealth;
    public float currentHealth;
    public HealthBar healthBar;
    public int coinToDrop;
    public int scoreToAdd;
    public int xpToAdd;

    public BossSpawner spawnBoss;
    
    private void Awake() 
    {
        spawnBoss = FindObjectOfType<BossSpawner>().GetComponent<BossSpawner>();
        
    }

    void Start()
    {
        //maxHealth = 1500f;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            //add score
            int score = PlayerPrefs.GetInt("CurrentScore") + scoreToAdd;
            PlayerPrefs.SetInt("CurrentScore", score);
            //add xp
            int xp = PlayerPrefs.GetInt("CurrentXP") + xpToAdd;
            PlayerPrefs.SetInt("CurrentXP", xp);
            //drop coins
            while (c < coinToDrop)
            {
                Coin drop = Instantiate(coin, transform.position, Quaternion.identity);
                c++;
            }
            if(this.gameObject.tag == "Boss")
            {
                //diamond drop
                int rand = Random.Range(0, 99);
                if(rand <= 49) //drop chance
                {
                    Coin drop = Instantiate(diamond, transform.position, Quaternion.identity);
                    
                }
                spawnBoss.SpawnBoss();
            }


            Destroy(gameObject);       // destroy the boss
            //PlayerPrefs.SetInt("BossDestroyed", 1);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            int damage = 50;
            if (currentHealth > 1)
            {
                healthBar.SetHealth(currentHealth);
                currentHealth -= damage;
                Destroy(other.gameObject);  //destroy bullet
            }
        }

        if (other.gameObject.tag == "Bullet2")
        {
            int damage = 50;
            if (currentHealth > 1)
            {
                transform.Translate(Vector2.up * 15 * Time.deltaTime);
                healthBar.SetHealth(currentHealth);
                currentHealth -= damage;
                Destroy(other.gameObject);  //destroy bullet

            }
        }

		if (other.gameObject.tag == "Bullet4")
		{
			int damage = 50;
			if (currentHealth > 1)
			{
				healthBar.SetHealth(currentHealth);
				currentHealth -= damage;
				//this.GetComponent<Bairus>().downSpeed = .5f;
				this.GetComponent<SpriteRenderer>().color = Color.cyan;
				Destroy(other.gameObject);  //destroy bullet
			}
		}

		if (other.gameObject.tag == "Bullet5")
		{
			int damage = 50;
			if (currentHealth > 1)
			{
				healthBar.SetHealth(currentHealth);
				currentHealth -= damage;
			}
		}

	}
}
