using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemSO", menuName = "ToTheMoon/ShopItemSO", order = 0)]
public class ShopItemSO : ScriptableObject
{
    public string title;
    public string description;
    public int currentPrice;
}
