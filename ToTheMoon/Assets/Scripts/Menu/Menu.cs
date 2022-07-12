using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public Text hp;
	public Text lvlText;
	public Text xpNeedToLevelUpText;
	public Text coinT;
	public Text diamondT;
	public Text hscore;
	public Text displayname;
	public Image dp;
	
	
	public PlayerData playerData;
	public FacebookLogin fbLogin;
	public HealthBar expiBar;
	
	// int level = 1;
	// int currentXP;
	// int targetXP = 100;
	// int xpNeedToLevelUp;
	
	private void Awake()
	{
		playerData.GetPlayerData();
		playerData.GetCoinDiamond();
		playerData.GetHighestScore();
		fbLogin = FindObjectOfType<FacebookLogin>().GetComponent<FacebookLogin>();
		displayname.text = playerData.displayname;
		dp.sprite = fbLogin.dp;
		playerData.AddXPToServer(0);
	}
	void Start()
    {
		
		
		expiBar.SetMaxHealth(playerData.targetXP);
		expiBar.SetHealth(playerData.xpNeedToLevelUp);
	}
	
	public void AddXPBtn()
	{
		playerData.AddXPToServer(5);
	}

    void Update()
    {
		hp.text = playerData._maxHealth.ToString();
		
		coinT.text = playerData._coins.ToString();
		diamondT.text = playerData._diamonds.ToString();
		hscore.text = "Highest Score: " + playerData._highestScore.ToString();
		xpNeedToLevelUpText.text = playerData.xpNeedToLevelUp.ToString();
		lvlText.text = " Level: " + playerData._level.ToString();
		
	}

	public void LeaderboardScene()
	{
		SceneManager.LoadScene("LeaderBoard");
	}

	public void GameScene()
	{
		SceneManager.LoadScene("Game");
	}
	
	public void ProfileScene()
	{
		SceneManager.LoadScene("Profile");
	}
	
	public void ShopScene()
	{
		SceneManager.LoadScene("Shop");
	}
	
	

	//public int goldAmount;
	

	

}
