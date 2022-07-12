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
	public PlayerData playerData;
	

	public string AuthTicket;
	public Sprite dp;
	public GameObject addDisplaynamePanel;

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
				// Panel_Add.SetActive(true);
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
		if (string.IsNullOrEmpty(result.PlayerProfile.DisplayName)) //Check if it is a new playfab player.
		{
			Debug.Log("New player registered!");
			playerData.RegisterPlayerData(100, 0);
			addDisplaynamePanel.SetActive(true);
			
			//StartCoroutine(LoadStartScene(5f));
		}
		else
		{
			Debug.Log("old player login!");
			//playerData.GetPlayerData();
			playerData.displayname = result.PlayerProfile.DisplayName;
			StartCoroutine(LoadStartScene(3f));
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
			//Debug.Log("" + name);
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
		}
		else
		{
			Debug.Log(result.Error);
		}
	}
	
	public IEnumerator LoadStartScene(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		SceneManager.LoadScene("Menu");
	}
}
