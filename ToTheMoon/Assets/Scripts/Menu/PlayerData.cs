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
	public int _expi;
	public int _coins;
	public int _diamonds;
	public int _highestScore;

	
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
			_expi = int.Parse(result.Data["Expi"].Value);
		}
		else
			Debug.Log("Player data not complete!");
	}

	private void OnError(PlayFabError error)
	{
		Debug.Log(error.GenerateErrorReport());
	}

	public void SavePlayerData(float health, int expi, int addCoin, int addDiamond)
	{
		var requestUserData = new UpdateUserDataRequest
		{
			Data = new Dictionary<string, string> {
				{"MaxHealth", (_maxHealth + health).ToString() },
				{"Expi", (_expi + expi).ToString() }
			}
		};
		PlayFabClientAPI.UpdateUserData(requestUserData, OnDataSend, OnError);
		
		// var requestVirtualCurrencyCoin = new AddUserVirtualCurrencyRequest { VirtualCurrency = "CO", Amount = addCoin };
		// PlayFabClientAPI.AddUserVirtualCurrency(requestVirtualCurrencyCoin, OnModifyCurrencyCoin, OnError);

		// var requestVirtualCurrencyDiamond = new AddUserVirtualCurrencyRequest { VirtualCurrency = "DI", Amount = addDiamond };
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
			_highestScore = result.Statistics[0].Value;
		}
		else
		{
			SetPlayerHighestScore(0);
		}
		
	}

	// private void OnErrorHS(PlayFabError error)
	// {
	// 	Debug.Log(error.GenerateErrorReport());
	// 	SetPlayerHighestScore(0);
	// }

	#endregion

	// ACCESS TOKEN 
	//EAAEWEtoY0ZBABAEZAcs8HvsiFWO06XJIqQc8BIrO2Sqc7crwNV43kZAiUZCO33nubYH84megQohW3PVy7R99GQGHNT089tq0uWDfYCgUFy0QlQYB7QHZBRQxcb2wpb22huLJ9FwDTpncJDoZBUgGkadd9tbGOaX7gs6sfZB7ARaHQZDZD

}
