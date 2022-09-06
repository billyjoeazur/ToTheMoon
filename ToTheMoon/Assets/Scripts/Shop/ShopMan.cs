using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMan : MonoBehaviour
{
    public string currentTab = "Advance";
    public event Action<string> OnHeaderButtonPress;
    
    public void GoTab(string tab)
    {
        currentTab = tab;
        OnHeaderButtonPress?.Invoke(currentTab);
    }
    
    private void Start() 
    {
        GoTab("Advance");
    }
    
    void OnEnable()
    {
        OnHeaderButtonPress += ChangeTab;
    }
    
    void OnDisable()
    {
        OnHeaderButtonPress -= ChangeTab;
    }
    
    void ChangeTab(string tab)
    {
        
    }
}
