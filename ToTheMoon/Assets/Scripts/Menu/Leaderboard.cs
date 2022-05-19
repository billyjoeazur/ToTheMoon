using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class Leaderboard : MonoBehaviour
{
	public GameObject listingPrefab;
	public Transform listingContainer;

	private void Start()
	{
		GetLeaderBoard();
	}

	public void GetLeaderBoard()
	{
		var requestLeaderboard = new GetLeaderboardRequest { StartPosition = 0, StatisticName = "HighestScore", MaxResultsCount = 10 };
		PlayFabClientAPI.GetLeaderboard(requestLeaderboard, OnGetLeaderboard, OnError);
	}

	private void OnGetLeaderboard(GetLeaderboardResult result)
	{
		foreach (PlayerLeaderboardEntry player in result.Leaderboard)
		{
			GameObject tempListing = Instantiate(listingPrefab, listingContainer);
			LeaderboardListing LL = tempListing.GetComponent<LeaderboardListing>();
			LL.playerName.text = player.DisplayName;
			LL.playerScore.text = player.StatValue.ToString();
			//Debug.Log($"{player.DisplayName} {player.StatValue}");
		}
	}

	private void OnError(PlayFabError error)
	{
		Debug.Log(error.GenerateErrorReport());
	}

	public void BackToStartMenuScene()
	{
		SceneManager.LoadScene("Start");
	}
}
