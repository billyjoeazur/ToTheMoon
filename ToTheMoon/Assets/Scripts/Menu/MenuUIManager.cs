using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] private Text lvlText;
    [SerializeField] private Text xpNeedToLevelUpText;
    [SerializeField] private Text coinText;
    [SerializeField] private Text diamondText;
    [SerializeField] private Text highscore;
    [SerializeField] private Text displayname;
    
    [SerializeField] private Slider expSlider;
    [SerializeField] private Image expFill;
    
    public PlayerSO playerSO;
    public ShopSO shopSO;
    
    void Start()
    {
        Time.timeScale = 1f;
        xpNeedToLevelUpText.text = playerSO.player.GetExperienceToLevelUp(playerSO.player).ToString();
        lvlText.text = "Level: " + playerSO.player.level.ToString();
        highscore.text = playerSO.highestScore.ToString();
        displayname.text = playerSO.player.displayname;
        
        coinText.text = shopSO._coins.ToString();
        diamondText.text = shopSO._diamonds.ToString();
        
        
        expSlider.maxValue = playerSO.player.targetExperience;
        expSlider.value = playerSO.player.GetExperienceToLevelUp(playerSO.player);
    }
    
    public void GameScene()
    {
        gameObject.GetComponent<SceneController>().GameScene();
    }
    
    public void ShopScene()
    {
        gameObject.GetComponent<SceneController>().ShopScene();
    }
    
    public void OpenChest()
    {
        PlayFabController.playFabController.GiveBasicChest();
        print("Chest Click");
    }
}
