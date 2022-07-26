using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;
using System;

public class PlayFabController : MonoBehaviour
{
	public static PlayFabController playFabController;
	
	public PlayerSO playerSO;
	public ShopSO shopSO;
	public Player playerRegister = new Player("", 100, 1, 0, 100, 0);
	public List<Spaceship> spaceshipsRegister = new List<Spaceship>();
	public Sprite avatar;
	
	private void Awake()
	{
		if (playFabController == null)
			playFabController = this;
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
		if (result.Data != null && result.Data.ContainsKey("Player") && result.Data.ContainsKey("Spaceships"))
		{
			playerSO.PlayerDataUpdate(JsonConvert.DeserializeObject<Player>(result.Data["Player"].Value));
			shopSO.SpaceshipUpdate(JsonConvert.DeserializeObject<List<Spaceship>>(result.Data["Spaceships"].Value));
		}
		else
			Debug.Log("Player data not complete!");
	}

	private void OnError(PlayFabError error)
	{
		Debug.Log(error.GenerateErrorReport());
	}
	
	public void RegisterPlayerData()
	{
		AddSpaceshipBaseData();
		var requestUserData = new UpdateUserDataRequest
		{
			Data = new Dictionary<string, string> {
				{"Player", JsonConvert.SerializeObject(playerRegister) },
				{"Spaceships", JsonConvert.SerializeObject(spaceshipsRegister) }
			}
		};
		PlayFabClientAPI.UpdateUserData(requestUserData, OnDataRegister, OnError);
		
		SetPlayerHighestScore(0);
	}
	
	private void OnDataRegister(UpdateUserDataResult result)
	{
		Debug.Log("Successful data Register!");
		GetPlayerData();
	}
	
	public void AddDisplayName(Text displaynameTxt)
	{
		PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest { DisplayName = displaynameTxt.text }, OnDisplayName, OnErrorDisplayname);
		
	}
	
	private void OnDisplayName(UpdateUserTitleDisplayNameResult result)
	{
		Debug.Log("Displayname added! " + result.DisplayName);
		playerSO.player.displayname = result.DisplayName;
		SavePlayerData();
		GameObject.Find("FacebookLogin").GetComponent<SceneController>().GoScene("Menu");
	}
	
	private void OnErrorDisplayname(PlayFabError error)
	{
		if (error.Error == PlayFabErrorCode.NameNotAvailable)
        {
            Debug.Log("Name is already taken, please pick another name");
            //your logic
        }
	}
	
	public void SavePlayerData()
	{
		var requestUserData = new UpdateUserDataRequest
		{
			Data = new Dictionary<string, string> {
				{"Player", JsonConvert.SerializeObject(playerSO.player) },
				{"Spaceships", JsonConvert.SerializeObject(shopSO.spaceships) } 
			}
		};
		PlayFabClientAPI.UpdateUserData(requestUserData, OnDataSave, OnError);
	}
	
	private void OnDataSave(UpdateUserDataResult result)
	{
		Debug.Log("Successful data Save!");
		GetPlayerData();
	}

	public void GetCoinDiamond()
	{
		PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnReceivedCoin, OnError);
	}

	private void OnReceivedCoin(GetUserInventoryResult result)
	{
		result.VirtualCurrency.TryGetValue("CO", out shopSO._coins);
		result.VirtualCurrency.TryGetValue("DI", out shopSO._diamonds);
		print("getcoindiamondmethod");
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
			playerSO.highestScore = result.Statistics[0].Value;
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
		shopSO._coins = result.Balance;
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
		shopSO._coins = result.Balance;
	}
	
	//add diamond
	public void AddDiamond(int diamondAmount)
	{
		var requestDiamond = new AddUserVirtualCurrencyRequest{
			VirtualCurrency = "DI",
			Amount = diamondAmount
		};
		PlayFabClientAPI.AddUserVirtualCurrency(requestDiamond, OnDiamondSuccess, OnError);
	}
	
	private void OnDiamondSuccess(ModifyUserVirtualCurrencyResult result)
	{
		Debug.Log("Diamond Added");
		shopSO._diamonds = result.Balance;
	}
	
	public void AddSpaceshipBaseData()
    {
        spaceshipsRegister.Add(new Spaceship("SS-Mars", "Spaceship Mars", 1, 5));
        spaceshipsRegister.Add(new Spaceship("HWSS-Mani", "Heavy Weight Spaceship Mani", 0, 10));
        spaceshipsRegister.Add(new Spaceship("LWSS-Edsa", "Light Weight Spaceship Edsa", 0, 15));
        spaceshipsRegister.Add(new Spaceship("BSS", "Battle Spaceship", 0, 20));
        spaceshipsRegister.Add(new Spaceship("HMS-Marites", "Her Majesty's Ship Marites", 0, 25));
        spaceshipsRegister.Add(new Spaceship("ISS-Digs", "Imperial Spaceship Digs", 0, 30));
    }
	
	#endregion

	
}
