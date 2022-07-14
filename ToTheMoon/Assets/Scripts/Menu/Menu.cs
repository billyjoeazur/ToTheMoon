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
	
	private void Awake()
	{
		playerData.GetPlayerData();
		playerData.GetCoinDiamond();
		playerData.GetHighestScore();
		fbLogin = FindObjectOfType<FacebookLogin>().GetComponent<FacebookLogin>();
		displayname.text = playerData.player.displayname;
		dp.sprite = fbLogin.dp;
	}
	void Start()
    {
		PlayerPrefs.SetInt("CurrentSpaceship", playerData.player.equipedSpaceship);
		print(playerData.player.displayname);
	}
	
    void Update()
    {
		UpdateUI();
	}
	
	void UpdateUI()
	{
		hp.text = playerData.player.maxHealth.ToString();
		coinT.text = playerData._coins.ToString();
		diamondT.text = playerData._diamonds.ToString();
		hscore.text = "Highest Score: " + playerData._highestScore.ToString();
		lvlText.text = " Level: " + playerData.player.level.ToString();
		xpNeedToLevelUpText.text = playerData.player.GetExperienceToLevelUp(playerData.player).ToString();
		expiBar.SetMaxHealth(playerData.player.targetExperience);
		expiBar.SetHealth(playerData.player.GetExperienceToLevelUp(playerData.player));
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
}
