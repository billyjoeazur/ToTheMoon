using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneController : MonoBehaviour
{
    public void MenuScene()
    {
        StartCoroutine(MainMenu(4f));
    }
    
    public void ShopScene()
    {
        StartCoroutine(Shop(2f));
    }
    
    public void GameScene()
    {
        StartCoroutine(Game(2f));
    }
    
    public IEnumerator MainMenu(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PlayFabController.playFabController.GetPlayerData();
        PlayFabController.playFabController.GetCoinDiamond();
        PlayFabController.playFabController.GetHighestScore();
        SceneManager.LoadScene("Menu");
    }
    
    public IEnumerator Shop(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Shop");
    }
    
    public IEnumerator Game(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Game");
    }
    
    //EAAEWEtoY0ZBABAF18AUmdolVhAhFJWa7frtOgSNXoRJnFd7VW1ir8zBWCnyXmqOWZCpSCCfWWsZA1ZActaEQiXm4mapZAVt3tE2s5ZCPOvKZBGdN6ZAT2DvZBzPTZAaZC4ZBpmxe43hxep2tXGjA6eEWEgrnZBn7SZB2ZBdoZAeFz9pQZBEJzYoNd1ZBX2XcyipYXw7AEtZBW7v3sRobdtY3CyyHNID0ZAFn
    
    // public IEnumerator Delay(float waitTime, string sceneName)
    // {
    //     if (sceneName == "Menu")
    //     {
    //         PlayFabController.playFabController.GetPlayerData();
    //         PlayFabController.playFabController.GetCoinDiamond();
    //         PlayFabController.playFabController.GetHighestScore();
    //     }
    //     yield return new WaitForSeconds(waitTime);
    //     print("waiting...");
    //     SceneManager.LoadScene(sceneName);
    // }
    
    
    
    }
