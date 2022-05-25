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

    void Awake()
    {
        maxHealth = playerData._maxHealth;
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        StartCoroutine(Damage(3f));
    }

    void TakeDamage()
    {
        currentHealth -= 0.1f;
        healthBar.SetHealth(currentHealth);
    }

    IEnumerator Damage(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        TakeDamage();
    }
}
