using System;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;

public class PurchaseItem : MonoBehaviour
{
    public void BuyItemInPlayFab()
    {
        string itemId = "BonusHP1";
        int quantity = 1;
        PlayFabClientAPI.PurchaseItem(new PurchaseItemRequest()
        {
            ItemId = itemId,
            //Quantity = quantity,
            VirtualCurrency = "CO"
        }, (result) => 
        {
            Debug.Log("Successfully purchased item");
        }, (error) => 
        {
            Debug.LogError("Failed to purchase item: " + error.GenerateErrorReport());
        });
    }
}
