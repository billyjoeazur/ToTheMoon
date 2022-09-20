using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerSO playerSO;
    public float currentHealth;
    
    [HideInInspector] public int coin, diamond, score, expi, diamondDropChance;
    private bool isNewHighestScore = false;
    public event Action<float> OnHealthChanged;
    public event Action<int> OnCoinChanged;
    public event Action<int> OnDiamondChanged;
    public event Action<int> OnScoreChanged;
    public event Action<int> OnExpiChanged;
    public event Action<bool> OnGameOver;
    
    [SerializeField] RewardedAdsButton rewardedAdsButton;
    public GameObject playerGO;
    void Start()
    {
        Time.timeScale = 1f;
        currentHealth = playerSO.player.maxHealth;
        diamondDropChance = playerSO.diamondDropChance;
        playerGO.transform.GetChild(playerSO.player.equipedSpaceship).gameObject.SetActive(true);
        coin = 0;
        diamond = 0;
        score = 0;
        expi = 0;
        OnHealthChanged?.Invoke(currentHealth);
        OnCoinChanged?.Invoke(coin);
        OnDiamondChanged?.Invoke(diamond);
        OnScoreChanged?.Invoke(score);
        OnExpiChanged?.Invoke(expi);
        InvokeRepeating("AutoHealPlayer", 1f, 1f);
    }
    
    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        OnHealthChanged?.Invoke(currentHealth);
        if (currentHealth <= 0)
        {
            GameOver(IsNewHighest());
        }
    }
    
    public void AddCoin(int amount)
    {
        amount *= playerSO.goldMultiplier;
        coin += amount;
        OnCoinChanged?.Invoke(coin);
    }
    
    public void AddDiamond(int amount)
    {
        diamond += amount;
        OnDiamondChanged?.Invoke(diamond);
    }
    
    public void AddScore(int amount)
    {
        score += amount;
        OnScoreChanged?.Invoke(score);
    }
    
    public void AddExpi(int amount)
    {
        expi += amount;
        OnExpiChanged?.Invoke(expi);
    }
    
    public void GameOver(bool isNewHighest)
    {
        Time.timeScale = 0f;
        OnGameOver?.Invoke(isNewHighest);
        rewardedAdsButton.LoadAd();//load ads
        UpdatePlayfabData();
    }
    
    public bool IsNewHighest()
    {
        if (score > playerSO.highestScore)
        {
            PlayFabController.playFabController.SetPlayerHighestScore(score);
            return true;
        }
        return false;
    }
    
    public void UpdatePlayfabData()
    {
        PlayFabController.playFabController.AddCoin(coin);
        PlayFabController.playFabController.AddDiamond(diamond);
        playerSO.player.experience += expi;
        playerSO.player.GetExperienceToLevelUp(playerSO.player); // add the level and experience
        PlayFabController.playFabController.SavePlayerData();
    }
    
    private void AutoHealPlayer()
    {
        if(currentHealth < playerSO.player.maxHealth)
        {
            currentHealth += playerSO.regenHP;
            OnHealthChanged?.Invoke(currentHealth);
        }
    }
    
    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

}
