using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using System;
using Facebook.Unity;

public class FacebookLogin : MonoBehaviour
{
	public static FacebookLogin instance;

	public string AuthTicket;
	public Sprite dp;
	public GameObject addDisplaynamePanel;
	//public SceneController sceneController;
	
	public PlayerSO playerSO;

	private void Awake()
	{
		instance = this;
		FB.Init(OnFBInitComplete, OnFBHideUnity);
	}

	public void OnLoginWithFacebookClicked()
	{
		FB.LogInWithReadPermissions(new List<string>() { "public_profile", "user_friends" }, OnHandleFBResult);
	}

	private void OnHandleFBResult(ILoginResult result)
	{
		if (result.Cancelled)
		{
			Debug.Log("Facebook login cancelled!");
			
		}
		else if (result.Error != null)
		{
			Debug.Log("Error found! " + result.Error);
		}
		else
		{
			if (FB.IsLoggedIn)
			{
				Debug.Log("Facebook is Login!");
			}
			else
			{
				
			}
			DealWithFbMenus(FB.IsLoggedIn);
			AuthTicket = result.AccessToken.TokenString;
			PlayerPrefs.SetString("FBTOKEN", AuthTicket);
			Debug.Log("Facebook login success!");
			AuthenticateFacebook();
		}
	}

	private void OnFBInitComplete()
	{
		if(AccessToken.CurrentAccessToken != null)
		{
			AuthTicket = AccessToken.CurrentAccessToken.TokenString;
		}
		DealWithFbMenus(FB.IsLoggedIn);
	}

	private void OnFBHideUnity(bool isUnityShown)
	{
		throw new NotImplementedException();
	}

	public void AuthenticateFacebook()
	{
		if (FB.IsInitialized && FB.IsLoggedIn && !string.IsNullOrEmpty(AuthTicket))
		{
			PlayFabClientAPI.LoginWithFacebook(new LoginWithFacebookRequest()
			{
				AccessToken = AuthTicket,
				CreateAccount = true

			}, OnLoginSuccess, OnError);

		}
	}

	private void OnLoginSuccess(PlayFab.ClientModels.LoginResult result)
	{
		Debug.Log("Login in playfab success!");
		PlayFabClientAPI.GetPlayerProfile(new GetPlayerProfileRequest()
		{
			ProfileConstraints = new PlayerProfileViewConstraints(){
				ShowDisplayName = true
			}
		}, 
		OnGetDisplayName, OnError);
		
	}

	private void OnGetDisplayName(GetPlayerProfileResult result)
	{
		Time.timeScale = 1f;
		if (string.IsNullOrEmpty(result.PlayerProfile.DisplayName)) //Check if it is a new playfab player.
		{
			Debug.Log("New player registered!");
			PlayFabController.playFabController.RegisterPlayerData();
			addDisplaynamePanel.SetActive(true);
		}
		else
		{
			Debug.Log("old player login!");
			gameObject.GetComponent<SceneController>().MenuScene();
		}
		
	}

	private void OnError(PlayFabError error)
	{
		Debug.Log(error.GenerateErrorReport());
	}

	public void DealWithFbMenus(bool isLoggedIn)
	{
		if (isLoggedIn)
		{
			FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
			FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
		}

		else
		{

		}
	}

	void DisplayUsername(IResult result)
	{
		if (result.Error == null)
		{
			string name = "" + result.ResultDictionary["first_name"];
			//playerData.displayname = name;
		}
		else
		{
			Debug.Log(result.Error);
		}
	}

	void DisplayProfilePic(IGraphResult result)
	{
		if (result.Texture != null)
		{
			Debug.Log("Profile Pic");
			dp = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
			PlayFabController.playFabController.avatar = dp;
		}
		else
		{
			Debug.Log(result.Error);
		}
	}
	
	public IEnumerator Delay(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		
	}
	
}
