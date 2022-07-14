using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class GameMenu : MonoBehaviour
{
    public Text scoreText, highscoreText, coins, diamonds, expi;
    public Character character;
    public PlayerData playerData;
    
    [SerializeField] RewardedAdsButton rewardedAdsButton;
    bool _isDoneLoad = false;
    bool _coinAdded = false;
    bool _diamondAdded = false;
    bool _expiAdded = false;
    
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
            coins.text = "GOLD: " + character.currentCoin.ToString();
            diamonds.text = "DIAMOND: " + character.currentDiamond.ToString();
            expi.text = PlayerPrefs.GetInt("CurrentXP").ToString();
            
            //add new highscore
            if (character.isNewHighscore)
            {
                playerData.SetPlayerHighestScore(playerData._highestScore);
                character.isNewHighscore = false;
            }
            
            //add goldcoin
            if (!_coinAdded)
            {
                playerData.AddCoin(character.currentCoin);
                _coinAdded = true;
            }
            
            //add diamond
            if (!_diamondAdded)
            {
                playerData.AddDiamond(character.currentDiamond);
                _diamondAdded = true;
            }
            
            if (!_expiAdded)
            {
                //playerData.AddXPToServer(PlayerPrefs.GetInt("CurrentXP"));
                playerData.player.experience += PlayerPrefs.GetInt("CurrentXP");
                playerData.SavePlayerData();
                _expiAdded = true;
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
