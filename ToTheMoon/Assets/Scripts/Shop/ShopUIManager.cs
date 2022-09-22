using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class ShopUIManager : MonoBehaviour
{
    public PlayerSO playerSO;
    public ShopSO shopSO;
    public ShopManager man;
    public ShopTemplate[] shopPanels;
    public GameObject[] shopPanelsGO;
    public Button[] buyBtn;
    public Text coinsTxt;
    public Text diamondsTxt;
    string fieldtab = "";
    
    void OnEnable()
    {
        man.OnHeaderButtonPress += LoadAvailableItems;
        man.OnHeaderButtonPress += CheckPurchaseable;
    }
    void OnDisable()
    {
        man.OnHeaderButtonPress -= LoadAvailableItems;
        man.OnHeaderButtonPress -= CheckPurchaseable;
    }
    
    private void Awake() 
    {
        UpdateUI();
        //CheckPurchaseable();
    }
    
    private void LoadAvailableItems(string tab)
    {
        fieldtab = tab;
        if(tab == "Advance")
        {
            for (int i = 0; i < shopSO.spaceships.Count; i++)
            {
                shopPanelsGO[i].SetActive(true);
                shopPanels[i].titleTxt.text = shopSO.spaceships[i].title;
                shopPanels[i].descriptionTxt.text = shopSO.spaceships[i].description;
                shopPanels[i].levelTxt.text = shopSO.spaceships[i].level.ToString();
                shopPanels[i].currentPriceTxt.text = shopSO.spaceships[i].currentPrice.ToString();
                print("Advance");
            }
            
        }
        else if(tab == "Essentials")
        {
            for (int i = 0; i < shopSO.essentials.Count; i++)
            {
                shopPanelsGO[i].SetActive(true);
                shopPanels[i].titleTxt.text = shopSO.essentials[i].title;
                shopPanels[i].descriptionTxt.text = shopSO.essentials[i].description;
                shopPanels[i].levelTxt.text = shopSO.essentials[i].level.ToString();
                shopPanels[i].currentPriceTxt.text = shopSO.essentials[i].currentPrice.ToString();
                print("Essentials");
            }
            shopPanelsGO[5].SetActive(false); //remove template
            shopPanelsGO[6].SetActive(false); //remove template
        }
        else
        {
            for (int i = 0; i < shopSO.extras.Count; i++)
            {
                shopPanelsGO[i].SetActive(true);
                shopPanels[i].titleTxt.text = shopSO.extras[i].title;
                shopPanels[i].descriptionTxt.text = shopSO.extras[i].description;
                shopPanels[i].levelTxt.text = shopSO.extras[i].level.ToString();
                shopPanels[i].currentPriceTxt.text = shopSO.extras[i].currentPrice.ToString();
                print("extras");
            }
            shopPanelsGO[3].SetActive(false); //remove template
            shopPanelsGO[4].SetActive(false); //remove template
            shopPanelsGO[5].SetActive(false); //remove template
            shopPanelsGO[6].SetActive(false); //remove template
        }
        
        
    }
    
    private void CheckPurchaseable(string tab)
    {
        fieldtab = tab;
        if (tab == "Advance")
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
        else if(tab == "Essentials")
        {
            for (int i = 0; i < shopSO.essentials.Count; i++)
            {
                if(shopSO._coins >= shopSO.essentials[i].currentPrice)
                {
                    buyBtn[i].interactable = true;
                }
                else
                {
                    buyBtn[i].interactable = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < shopSO.extras.Count; i++)
            {
                if(shopSO._coins >= shopSO.extras[i].currentPrice)
                {
                    buyBtn[i].interactable = true;
                }
                else
                {
                    buyBtn[i].interactable = false;
                }
            }
        }
        
        
    }
    
    public void PurchaseItem(int btnNo)
    {
        if (fieldtab == "Advance")
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
        else if(fieldtab == "Essentials")
        {
            if (shopSO._coins >= shopSO.essentials[btnNo].currentPrice)
            {
                shopSO._coins -= shopSO.essentials[btnNo].currentPrice;
                PlayFabController.playFabController.SubtractCoin(shopSO.essentials[btnNo].currentPrice);
                shopSO.essentials[btnNo].currentPrice += shopSO.essentials[btnNo].currentPrice * 3;
                shopSO.essentials[btnNo].level++;
                if (btnNo == 0)
                {
                    playerSO.moreHP += 10f;
                }
                else if(btnNo == 1)
                {
                    playerSO.regenHP += .2f;
                }
                else if(btnNo == 2)
                {
                    playerSO.shieldCD -= 2f;
                }
                else if(btnNo == 3)
                {
                    playerSO.goldMultiplier += 1;
                }
                else if(btnNo == 4)
                {
                    playerSO.diamondDropChance += 2;
                }
                
                
                PlayFabController.playFabController.SavePlayerData();
                for (int i = 0; i < shopSO.essentials.Count; i++)
                {
                    buyBtn[i].interactable = false;
                }
                StartCoroutine(WaitResponse(3f));
                UpdateUI();
                
                shopPanels[btnNo].currentPriceTxt.text = shopSO.essentials[btnNo].currentPrice.ToString();
                shopPanels[btnNo].levelTxt.text = shopSO.essentials[btnNo].level.ToString();
                
            }
        }
        else
        {
            if (shopSO._coins >= shopSO.extras[btnNo].currentPrice)
            {
                shopSO._coins -= shopSO.extras[btnNo].currentPrice;
                PlayFabController.playFabController.SubtractCoin(shopSO.extras[btnNo].currentPrice);
                shopSO.extras[btnNo].currentPrice += shopSO.extras[btnNo].currentPrice * 3;
                shopSO.extras[btnNo].level++;
                //playerSO.player.equipedSpaceship = btnNo;
                PlayFabController.playFabController.SavePlayerData();
                for (int i = 0; i < shopSO.extras.Count; i++)
                {
                    buyBtn[i].interactable = false;
                }
                StartCoroutine(WaitResponse(3f));
                UpdateUI();
                
                shopPanels[btnNo].currentPriceTxt.text = shopSO.extras[btnNo].currentPrice.ToString();
                shopPanels[btnNo].levelTxt.text = shopSO.extras[btnNo].level.ToString();
                
            }
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
        if (fieldtab == "Advance")
        {
            for (int i = 0; i < shopSO.spaceships.Count; i++)
            {
                buyBtn[i].interactable = true;
            }
        }
        else if(fieldtab == "Essentials")
        {
            for (int i = 0; i < shopSO.essentials.Count; i++)
            {
                buyBtn[i].interactable = true;
            }
        }
        else
        {
            for (int i = 0; i < shopSO.extras.Count; i++)
            {
                buyBtn[i].interactable = true;
            }
        }
        
        CheckPurchaseable(fieldtab);
    }
    
    public void Back()
    {
        gameObject.GetComponent<SceneController>().MenuScene();
    }
}
