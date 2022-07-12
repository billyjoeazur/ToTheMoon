using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;
using System;

public class PlayerData : MonoBehaviour
{
	public static PlayerData instance;

	public string displayname;
	public float _maxHealth;
	public int _coins;
	public int _diamonds;
	public int _highestScore;
	public int _level;
	public int _totalXP;
	public int xpNeedToLevelUp;
	public int targetXP;
	public List<Spaceship> spaceships = new List<Spaceship>();
	
	
	
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
			spaceships = JsonConvert.DeserializeObject<List<Spaceship>>(result.Data["Spaceships"].Value);
		}
		else
			Debug.Log("Player data not complete!");
	}

	private void OnError(PlayFabError error)
	{
		Debug.Log(error.GenerateErrorReport());
	}
	
	public void RegisterPlayerData(float health, int expi)
	{
		ResetData();
		AddSpaceshipBaseData();
		var requestUserData = new UpdateUserDataRequest
		{
			Data = new Dictionary<string, string> {
				{"MaxHealth", health.ToString() },
				{"Expi", expi.ToString() },
				{"Spaceships", JsonConvert.SerializeObject(spaceships) }
			}
		};
		PlayFabClientAPI.UpdateUserData(requestUserData, OnDataRegister, OnError);
		
		
		SetPlayerHighestScore(0);
	}
	
	private void OnDataRegister(UpdateUserDataResult result)
	{
		Debug.Log("Successful data Register!");
		//GetPlayerData();
	}
	
	public void AddDisplayName(Text displaynameTxt)
	{
		PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest { DisplayName = displaynameTxt.text }, OnDisplayName, OnErrorDisplayname);
	}
	
	private void OnDisplayName(UpdateUserTitleDisplayNameResult result)
	{
		Debug.Log("Displayname added! " + result.DisplayName);
		displayname = result.DisplayName;
		SceneManager.LoadScene("Menu");
	}
	
	private void OnErrorDisplayname(PlayFabError error)
	{
		if (error.Error == PlayFabErrorCode.NameNotAvailable)
        {
            Debug.Log("Name is already taken, please pick another name");
            //your logic
        }
	}
	
	public void SavePlayerData(float health, int expi)
	{
		var requestUserData = new UpdateUserDataRequest
		{
			Data = new Dictionary<string, string> {
				{"MaxHealth", health.ToString() },
				{"Expi", expi.ToString() },
				{"Spaceships", JsonConvert.SerializeObject(spaceships) }
			}
		};
		PlayFabClientAPI.UpdateUserData(requestUserData, OnDataSave, OnError);
	}
	
	private void OnDataSave(UpdateUserDataResult result)
	{
		Debug.Log("Successful data Save!");
		//GetPlayerData();
	}
	


	public void GetCoinDiamond()
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
	
	//add gold
	public void AddCoin(int goldAmount)
	{
		var requestGold = new AddUserVirtualCurrencyRequest{
			VirtualCurrency = "CO",
			Amount = goldAmount
		};
		PlayFabClientAPI.AddUserVirtualCurrency(requestGold, OnCoinAddSuccess, OnError);
	}
	
	private void OnCoinAddSuccess(ModifyUserVirtualCurrencyResult result)
	{
		Debug.Log("Coin Added");
	}
	
	public void SubtractCoin(int goldAmount)
	{
		var requestGold = new SubtractUserVirtualCurrencyRequest{
			VirtualCurrency = "CO",
			Amount = goldAmount
		};
		PlayFabClientAPI.SubtractUserVirtualCurrency(requestGold, OnCoinSubtractSuccess, OnError);
	}
	
	private void OnCoinSubtractSuccess(ModifyUserVirtualCurrencyResult result)
	{
		Debug.Log("Coin Subtracted");
		//result.VirtualCurrency.TryGetValue("CO", out _coins);
		_coins = result.Balance;
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
	
	
	IEnumerator SceneMenu(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		
	}
	
	public void AddSpaceshipBaseData()
    {
        spaceships.Add(new Spaceship("SS-Mars", "Spaceship Mars", 1, 5));
        spaceships.Add(new Spaceship("HWSS-Mani", "Heavy Weight Spaceship Mani", 0, 10));
        spaceships.Add(new Spaceship("LWSS-Edsa", "Light Weight Spaceship Edsa", 0, 15));
        spaceships.Add(new Spaceship("BSS", "Battle Spaceship", 0, 20));
        spaceships.Add(new Spaceship("HMS-Marites", "Her Majesty's Ship Marites", 0, 25));
        spaceships.Add(new Spaceship("ISS-Digs", "Imperial Spaceship Digs", 0, 30));
    }
	
	void ResetData()
	{
		_maxHealth = 100;
		_totalXP = 0;
		_coins = 0;
		_diamonds = 0;
		_highestScore = 0;
		_level = 0;
		xpNeedToLevelUp = 0;
		targetXP = 0;
		spaceships.Clear();
		
	}
	



	#endregion

	
}
