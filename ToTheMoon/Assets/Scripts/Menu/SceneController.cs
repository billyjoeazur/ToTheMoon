using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneController : MonoBehaviour
{
    [NonSerialized]
    public UnityEvent<string> OnGoScene;
    
    public void GoScene(string sceneName)
    {
        StartCoroutine(Delay(4f, sceneName));
        OnGoScene?.Invoke(sceneName);
    }
    
    public IEnumerator Delay(float waitTime, string sceneName)
    {
        if (sceneName == "Menu")
        {
            PlayFabController.playFabController.GetPlayerData();
            PlayFabController.playFabController.GetCoinDiamond();
            PlayFabController.playFabController.GetHighestScore();
        }
        yield return new WaitForSeconds(waitTime);
        print("waiting...");
        SceneManager.LoadScene(sceneName);
    }
    
    
}
