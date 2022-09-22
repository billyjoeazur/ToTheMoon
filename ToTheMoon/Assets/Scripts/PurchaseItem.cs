using System;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;

public class PurchaseItem : MonoBehaviour
{
    public List<Inventory> inventory;
    private void Start() 
    {
        
    }
    
    public void BuyItemInPlayFab()
    {
        PurchaseItemRequest request = new PurchaseItemRequest();
        request.CatalogVersion = "Items";
        request.ItemId = "BonusHP1";
        request.VirtualCurrency = "CO";
        request.Price = 100;
        
        PlayFabClientAPI.PurchaseItem(request, result => {
        }, error => {
            Debug.Log(error.ErrorMessage);
        });
    }
    
    public void GetInventory()
    {
        GetUserInventoryRequest request = new GetUserInventoryRequest();
        PlayFabClientAPI.GetUserInventory(request, result => {
            List<ItemInstance> myItems = result.Inventory;
            foreach (var item in myItems)
            {
                inventory.Add(new Inventory(item.ItemId, item.DisplayName));
            }
        }, error => {
            
        });
    }
}
