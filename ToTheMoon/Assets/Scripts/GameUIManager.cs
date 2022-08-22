using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Gradient healthGradient;
    [SerializeField] private Image healthFill;
    [SerializeField] private Text coinText, diamondText, scoreText, expiText;
    [SerializeField] private Text coinMenuText, diamondMenuText, scoreMenuText, highestScoreText, expiMenuText;
    public GameManager gameManager;
    [SerializeField] private GameObject menuPanel, newHighest;
    
    void OnEnable() 
    {
        gameManager.OnHealthChanged += ChangeHealthSliderValue;
        gameManager.OnCoinChanged += ChangeCoinValue;
        gameManager.OnDiamondChanged += ChangeDiamondValue;
        gameManager.OnScoreChanged += ChangeScoreValue;
        gameManager.OnExpiChanged += ChangeExpiValue;
        gameManager.OnGameOver += GameOver;
    }
    
    void OnDisable() 
    {
        gameManager.OnHealthChanged -= ChangeHealthSliderValue;
        gameManager.OnCoinChanged -= ChangeCoinValue;
        gameManager.OnDiamondChanged -= ChangeDiamondValue;
        gameManager.OnScoreChanged -= ChangeScoreValue;
        gameManager.OnExpiChanged -= ChangeExpiValue;
        gameManager.OnGameOver -= GameOver;
    }
    
    public void ChangeHealthSliderValue(float amount)
    {
        healthSlider.value = amount;
        healthFill.color = healthGradient.Evaluate(healthSlider.normalizedValue);
    }
    
    private void ChangeCoinValue(int amount)
    {
        coinText.text = amount.ToString();
        coinMenuText.text = amount.ToString();
    }
    
    private void ChangeDiamondValue(int amount)
    {
        diamondText.text = amount.ToString();
    }
    
    private void ChangeScoreValue(int amount)
    {
        scoreText.text = amount.ToString();
    }
    
    private void ChangeExpiValue(int amount)
    {
        expiText.text = amount.ToString();
    }
    
    private void GameOver(bool isNewHighest)
    {
        menuPanel.SetActive(true);
        //Time.timeScale = 0f;
        
        //coinMenuText.text = coinText.text;
        diamondMenuText.text = diamondText.text;
        scoreMenuText.text = scoreText.text;
        highestScoreText.text = gameManager.playerSO.highestScore.ToString();
        expiMenuText.text = expiText.text;
        if (isNewHighest)
        {
            newHighest.SetActive(true);
            highestScoreText.text = scoreText.text;
        }
    }
    
    
}
