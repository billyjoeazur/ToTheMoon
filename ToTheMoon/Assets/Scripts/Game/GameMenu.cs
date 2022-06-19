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
    public Player player;
    public PlayerData playerData;
    
    [SerializeField] RewardedAdsButton rewardedAdsButton;
    bool _isDoneLoad = false;
    bool _coinAdded = false;
    bool _diamondAdded = false;
    //bool _highScoreAdded = false;
    void Start()
    {
        
    }

    void Update()
    {
        if(this.gameObject.activeSelf)
        {
            //load the rewarded add
            if (!_isDoneLoad)
            {
                rewardedAdsButton.LoadAd();
                _isDoneLoad = true;
            }
            
            scoreText.text = "SCORE: " + PlayerPrefs.GetInt("CurrentScore").ToString();
            highscoreText.text =  "HIGHSCORE: " + playerData._highestScore.ToString();
            coins.text = "GOLD: " + player.currentCoin.ToString();
            diamonds.text = "DIAMOND: " + player.currentDiamond.ToString();
            
            //add new highscore
            if (player.isNewHighscore)
            {
                playerData.SetPlayerHighestScore(playerData._highestScore);
                player.isNewHighscore = false;
            }
            
            //add goldcoin
            if (!_coinAdded)
            {
                playerData.AddCoin(player.currentCoin);
                _coinAdded = true;
            }
            
            //add diamond
            if (!_diamondAdded)
            {
                playerData.AddDiamond(player.currentDiamond);
                _diamondAdded = true;
            }
            
            
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
