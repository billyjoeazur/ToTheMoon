using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public ShopTemplate[] shopPanels;
    public GameObject[] shopPanelsGO;
    public Button[] buyBtn;
    public PlayerData playerData;
    public Text coinsTxt;
    public Text diamondsTxt;
    
    private void Awake() 
    {
        UpdateUI();
        CheckPurchaseable();
    }
    
    private void Start() 
    {
        LoadAvailableItems();
    }
    
    private void LoadAvailableItems()
    {
        for (int i = 0; i < playerData.spaceships.Count; i++)
        {
            shopPanelsGO[i].SetActive(true);
            shopPanels[i].titleTxt.text = playerData.spaceships[i].title;
            shopPanels[i].descriptionTxt.text = playerData.spaceships[i].description;
            shopPanels[i].levelTxt.text = playerData.spaceships[i].level.ToString();
            shopPanels[i].currentPriceTxt.text = playerData.spaceships[i].currentPrice.ToString();
        }
    }
    
    private void CheckPurchaseable()
    {
        for (int i = 0; i < playerData.spaceships.Count; i++)
        {
            if(playerData._coins >= playerData.spaceships[i].currentPrice)
            {
                buyBtn[i].interactable = true;
            }
            else
            {
                buyBtn[i].interactable = false;
            }
        }
    }
    
    public void PurchaseItem(int btnNo)
    {
        if (playerData._coins >= playerData.spaceships[btnNo].currentPrice)
        {
            playerData._coins -= playerData.spaceships[btnNo].currentPrice;
            playerData.SubtractCoin(playerData.spaceships[btnNo].currentPrice);
            playerData.spaceships[btnNo].currentPrice += playerData.spaceships[btnNo].currentPrice * 3;
            playerData.spaceships[btnNo].level++;
            playerData.player.equipedSpaceship = btnNo;
            playerData.SavePlayerData();
            for (int i = 0; i < playerData.spaceships.Count; i++)
            {
                buyBtn[i].interactable = false;
            }
            StartCoroutine(WaitResponse(1f));
            UpdateUI();
            
            shopPanels[btnNo].currentPriceTxt.text = playerData.spaceships[btnNo].currentPrice.ToString();
            shopPanels[btnNo].levelTxt.text = playerData.spaceships[btnNo].level.ToString();
            
        }
    }
    
    private void UpdateUI()
    {
        coinsTxt.text = playerData._coins.ToString();
        diamondsTxt.text = playerData._diamonds.ToString();
        
    }
    
    IEnumerator WaitResponse(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        for (int i = 0; i < playerData.spaceships.Count; i++)
        {
            buyBtn[i].interactable = true;
        }
        CheckPurchaseable();
    }
    
    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }
}
