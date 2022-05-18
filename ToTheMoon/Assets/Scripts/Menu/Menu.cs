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
	public Text xp;
	public Text coinT;
	public Text diamondT;
	public Text hscore;
	public Text displayname;
	public Image dp;
	public int level;
	public int expi;
	
	public PlayerData playerData;
	public FacebookLogin fbLogin;
	
	private void Awake()
	{
		playerData.GetPlayerData();
		playerData.GetCoin();
		playerData.GetHighestScore();
		fbLogin = FindObjectOfType<FacebookLogin>().GetComponent<FacebookLogin>();
		displayname.text = playerData.displayname;
		dp.sprite = fbLogin.dp;
	}
	void Start()
    {
		LevelSystem();
	}

	void LevelSystem()
	{
		int[] lvl = {0, 100, 160, 256, 409};
		for (int i = 0; i < lvl.Length; i++)
		{
			if (playerData._expi > lvl[i])
			{
				playerData._expi -= lvl[i];
				level = i;
				
			}
			else
			{
				expi = lvl[i-1] - playerData._expi;
			}
		}
	}

    void Update()
    {
		hp.text = playerData._maxHealth.ToString();
		xp.text = playerData._expi.ToString();
		lvlText.text = level.ToString();
		//xp.text = expi.ToString();
		coinT.text = playerData._coins.ToString();
		diamondT.text = playerData._diamonds.ToString();
		hscore.text = playerData._highestScore.ToString();
	}

	public void addHPXP()
	{
		//playerData.SavePlayerData(0,0,0);
		AddCoin();
	}

	public void LeaderboardScene()
	{
		SceneManager.LoadScene("Leaderboard");
	}

	public void DemoScene()
	{
		SceneManager.LoadScene("DemoScene");
	}

	public void GameScene()
	{
		SceneManager.LoadScene("Game");
	}

	public int coinAmount;
	public void AddCoin()
	{
		var request = new AddUserVirtualCurrencyRequest{
			VirtualCurrency = "DI",
			Amount = coinAmount
		};
		PlayFabClientAPI.AddUserVirtualCurrency(request, OnAddCoinSuccess, OnError);

	}

	private void OnAddCoinSuccess(ModifyUserVirtualCurrencyResult result)
	{
		Debug.Log(coinAmount + " Coin Added");
	}

	private void OnError(PlayFabError error)
	{
		Debug.Log(error.GenerateErrorReport());
	}
}
