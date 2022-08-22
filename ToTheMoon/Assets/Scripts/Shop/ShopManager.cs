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
    public PlayerSO playerSO;
    public ShopSO shopSO;
    public ShopTemplate[] shopPanels;
    public GameObject[] shopPanelsGO;
    public Button[] buyBtn;
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
        print(shopSO.spaceships.Count);
    }
    
    private void LoadAvailableItems()
    {
        for (int i = 0; i < shopSO.spaceships.Count; i++)
        {
            shopPanelsGO[i].SetActive(true);
            shopPanels[i].titleTxt.text = shopSO.spaceships[i].title;
            shopPanels[i].descriptionTxt.text = shopSO.spaceships[i].description;
            shopPanels[i].levelTxt.text = shopSO.spaceships[i].level.ToString();
            shopPanels[i].currentPriceTxt.text = shopSO.spaceships[i].currentPrice.ToString();
        }
    }
    
    private void CheckPurchaseable()
    {
        for (int i = 0; i < shopSO.spaceships.Count; i++)
        {
            if(shopSO._coins >= shopSO.spaceships[i].currentPrice)
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
        if (shopSO._coins >= shopSO.spaceships[btnNo].currentPrice)
        {
            shopSO._coins -= shopSO.spaceships[btnNo].currentPrice;
            PlayFabController.playFabController.SubtractCoin(shopSO.spaceships[btnNo].currentPrice);
            shopSO.spaceships[btnNo].currentPrice += shopSO.spaceships[btnNo].currentPrice * 3;
            shopSO.spaceships[btnNo].level++;
            playerSO.player.equipedSpaceship = btnNo;
            PlayFabController.playFabController.SavePlayerData();
            for (int i = 0; i < shopSO.spaceships.Count; i++)
            {
                buyBtn[i].interactable = false;
            }
            StartCoroutine(WaitResponse(3f));
            UpdateUI();
            
            shopPanels[btnNo].currentPriceTxt.text = shopSO.spaceships[btnNo].currentPrice.ToString();
            shopPanels[btnNo].levelTxt.text = shopSO.spaceships[btnNo].level.ToString();
            
        }
    }
    
    private void UpdateUI()
    {
        coinsTxt.text = shopSO._coins.ToString();
        diamondsTxt.text = shopSO._diamonds.ToString();
        
    }
    
    IEnumerator WaitResponse(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        for (int i = 0; i < shopSO.spaceships.Count; i++)
        {
            buyBtn[i].interactable = true;
        }
        CheckPurchaseable();
    }
    
    public void Back()
    {
        gameObject.GetComponent<SceneController>().MenuScene();
    }
}
