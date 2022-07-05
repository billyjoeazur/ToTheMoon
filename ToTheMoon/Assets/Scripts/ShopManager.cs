using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public ShopItemSO[] shopItemsSO;
    public ShopTemplate[] shopPanels;
    public GameObject[] shopPanelsGO;
    public GameObject spaceships;
    
    
    public void LoadPanel()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            shopPanels[i].titleTxt.text = shopItemsSO[i].title;
            shopPanels[i].descriptionTxt.text = shopItemsSO[i].description;
            shopPanels[i].currentPriceTxt.text = shopItemsSO[i].currentPrice.ToString();
            //spaceships.transform.FindChild(shopItemsSO[i].title).gameObject.SetActive(true);
        }
    }
    
    private void Start() 
    {
        LoadAvailableItems();
        LoadPanel();
    }
    
    private void LoadAvailableItems()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            shopPanelsGO[i].SetActive(true);
            
        }
    }
}
