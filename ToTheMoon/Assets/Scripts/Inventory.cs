using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public string itemId; 
    public string displayName;
    
    public Inventory(string itemId, string displayName)
    {
        this.itemId = itemId;
        this.displayName = displayName;
    }
}
