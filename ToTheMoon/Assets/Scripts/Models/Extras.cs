using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extras
{
    public string title; 
    public string description;
    public int level;
    public int currentPrice;
    
    public Extras(string title, string description, int level, int currentPrice)
    {
        this.title = title;
        this.description = description;
        this.level = level;
        this.currentPrice = currentPrice;
    }
}
