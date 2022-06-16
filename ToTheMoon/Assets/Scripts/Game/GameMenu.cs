using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class GameMenu : MonoBehaviour
{
    public Text scoreText, highscoreText, coins, diamonds;
    public Player playerCurrency;
    
    [SerializeField] RewardedAdsButton rewardedAdsButton;
    bool isDoneLoad = false;
    void Start()
    {
        
    }

    void Update()
    {
        if(this.gameObject.activeSelf)
        {
            if (!isDoneLoad)
            {
                rewardedAdsButton.LoadAd();
                isDoneLoad = true;
            }
            
            scoreText.text = "SCORE: " + PlayerPrefs.GetInt("CurrentScore").ToString();
            highscoreText.text =  "HIGHSCORE: " + PlayerPrefs.GetInt("HighestScore").ToString();
            coins.text = "GOLD: " + playerCurrency.currentCoin.ToString();
            diamonds.text = "DIAMOND: " + playerCurrency.currentDiamond.ToString();
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
