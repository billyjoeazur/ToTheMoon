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
    public PlayerSO playerSO;
    
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
            highscoreText.text =  "HIGHSCORE: " + playerSO.highestScore.ToString();
            coins.text = "GOLD: " + character.currentCoin.ToString();
            diamonds.text = "DIAMOND: " + character.currentDiamond.ToString();
            expi.text = PlayerPrefs.GetInt("CurrentXP").ToString();
            
            //add new highscore
            if (character.isNewHighscore)
            {
                PlayFabController.playFabController.SetPlayerHighestScore(playerSO.highestScore);
                character.isNewHighscore = false;
            }
            
            //add goldcoin
            if (!_coinAdded)
            {
                PlayFabController.playFabController.AddCoin(character.currentCoin);
                _coinAdded = true;
            }
            
            //add diamond
            if (!_diamondAdded)
            {
                PlayFabController.playFabController.AddDiamond(character.currentDiamond);
                _diamondAdded = true;
            }
            
            if (!_expiAdded)
            {
                playerSO.player.experience += PlayerPrefs.GetInt("CurrentXP");
                playerSO.player.GetExperienceToLevelUp(playerSO.player); // add the level and experience
                PlayFabController.playFabController.SavePlayerData();
                _expiAdded = true;
            }
        }
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        this.gameObject.GetComponent<SceneController>().GameScene();
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        this.gameObject.GetComponent<SceneController>().MenuScene();
    }
}
