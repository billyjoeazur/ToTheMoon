using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class PlayerData : MonoBehaviour
{
	public static PlayerData instance;

	public string displayname;
	public float _maxHealth;
	//public int _expi;
	public int _coins;
	public int _diamonds;
	public int _highestScore;
	public int _level;
	public int _totalXP;
	
	
	
	private void Awake()
	{
		if (instance == null)
			instance = this;
		else
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
	}

	private void Start()
	{
		
	}

	#region

	public void GetPlayerData()
	{
		PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataRecieved, OnError);
	}

	private void OnDataRecieved(GetUserDataResult result)
	{
		Debug.Log("Received user data!");
		if (result.Data != null && result.Data.ContainsKey("MaxHealth") && result.Data.ContainsKey("Expi"))
		{
			_maxHealth = float.Parse(result.Data["MaxHealth"].Value);
			_totalXP = int.Parse(result.Data["Expi"].Value);
		}
		else
			Debug.Log("Player data not complete!");
	}

	private void OnError(PlayFabError error)
	{
		Debug.Log(error.GenerateErrorReport());
	}

	public void SavePlayerData(float health, int expi, int coin, int diamond)
	{
		var requestUserData = new UpdateUserDataRequest
		{
			Data = new Dictionary<string, string> {
				{"MaxHealth", (_maxHealth + health).ToString() },
				{"Expi", (_totalXP + expi).ToString() }
				
			}
		};
		PlayFabClientAPI.UpdateUserData(requestUserData, OnDataSend, OnError);
		
		// var requestVirtualCurrencyCoin = new AddUserVirtualCurrencyRequest { VirtualCurrency = "CO", Amount = coin };
		// PlayFabClientAPI.AddUserVirtualCurrency(requestVirtualCurrencyCoin, OnModifyCurrencyCoin, OnError);

		// var requestVirtualCurrencyDiamond = new AddUserVirtualCurrencyRequest { VirtualCurrency = "DI", Amount = diamond };
		// PlayFabClientAPI.AddUserVirtualCurrency(requestVirtualCurrencyDiamond, OnModifyCurrencyDiamond, OnError);
	}
	
	

	private void OnDataSend(UpdateUserDataResult result)
	{
		Debug.Log("Successful data send!");
		GetPlayerData();
	}

	// private void OnModifyCurrencyCoin(ModifyUserVirtualCurrencyResult result)
	// {
	// 	_coins = result.Balance;
	// }
	// private void OnModifyCurrencyDiamond(ModifyUserVirtualCurrencyResult result)
	// {
	// 	_diamonds = result.Balance;
	// }

	public void GetCoin()
	{
		PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnReceivedCoin, OnError);
	}

	private void OnReceivedCoin(GetUserInventoryResult result)
	{
		result.VirtualCurrency.TryGetValue("CO", out _coins);
		result.VirtualCurrency.TryGetValue("DI", out _diamonds);
	}

	public void SetPlayerHighestScore(int score)
	{
		PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
		{
			// request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required.
			Statistics = new List<StatisticUpdate>
			{
				new StatisticUpdate { StatisticName = "HighestScore", Value = score },
			}
		},
		result => { Debug.Log("User statistics updated " + score); },
		error => { Debug.LogError(error.GenerateErrorReport()); });
	}

	public void GetHighestScore()
	{
		GetPlayerStatisticsRequest requestHighestScore = new GetPlayerStatisticsRequest();
		PlayFabClientAPI.GetPlayerStatistics(requestHighestScore, OnRecievedScore, OnError);
	}

	private void OnRecievedScore(GetPlayerStatisticsResult result)
	{
		//check highestscore if null after monthly reset
		if(result.Statistics.Count > 0)
		{
			_highestScore = result.Statistics[1].Value;
		}
		else
		{
			SetPlayerHighestScore(0);
		}
	}
	
	//add gold
	public void AddCoin(int goldAmount)
	{
		var requestGold = new AddUserVirtualCurrencyRequest{
			VirtualCurrency = "CO",
			Amount = goldAmount
		};
		PlayFabClientAPI.AddUserVirtualCurrency(requestGold, OncoinSuccess, OnError);
	}
	
	private void OncoinSuccess(ModifyUserVirtualCurrencyResult result)
	{
		Debug.Log("Coin Added");
	}
	
	//add diamond
	public void AddDiamond(int diamondAmount)
	{
		var requestDiamond = new AddUserVirtualCurrencyRequest{
			VirtualCurrency = "DI",
			Amount = diamondAmount
		};
		PlayFabClientAPI.AddUserVirtualCurrency(requestDiamond, OndiamondSuccess, OnError);
	}
	
	private void OndiamondSuccess(ModifyUserVirtualCurrencyResult result)
	{
		Debug.Log("Diamond Added");
	}
	
	//add XP to server
	public void AddXPToServer(int expi)
	{
		
		var requestUserData = new UpdateUserDataRequest
		{
			Data = new Dictionary<string, string> {
				{"Expi", (_totalXP + expi).ToString() }
			}
		};
		PlayFabClientAPI.UpdateUserData(requestUserData, OnXPSend, OnError);
		
		
	}
	
	private void OnXPSend(UpdateUserDataResult result)
	{
		Debug.Log("Successful XP send!");
		
		LevelDesign();
		
		
	}
	
	public int xpNeedToLevelUp;
	public int targetXP;
	private void LevelDesign()
	{
		GetPlayerData();
		xpNeedToLevelUp = 0;
		targetXP = 100;
		_level = 1;
		while(_totalXP >= targetXP)
		{
			_totalXP = _totalXP - targetXP;
			_level++;
			targetXP += targetXP / 3;
			//targetXP = targetXP * 0.2f + targetXP;
		}
		xpNeedToLevelUp = targetXP - _totalXP;
	}
	
	
	
	
	//handle for level and expi needed
	// public void AddXP(int xpToAdd)
	// {
	// 	AddXPToServer(xpToAdd);
	// 	currentXP += xpToAdd;
	// 	_totalXP += xpToAdd;
	// 	while(currentXP >= targetXP)
	// 	{
	// 		currentXP = currentXP - targetXP;
	// 		_level++;
	// 		targetXP += targetXP / 3;
	// 		//targetXP = targetXP * 0.2f + targetXP;
	// 	}
	// 	xpNeedToLevelUp = targetXP - currentXP;
		
	// 	//SavePlayerData(_maxHealth, _level, _totalXP, _coins, _diamonds);
		
	// }
	



	#endregion

	
}
