using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ShopSO", menuName = "ToTheMoon/ShopSO", order = 1)]
public class ShopSO : ScriptableObject
{
    public List<Spaceship> spaceships = new List<Spaceship>();
    
    public int _coins;
	public int _diamonds;
    
    [NonSerialized]
    public UnityEvent<int> OnCurrencyUpdate;
    
    [NonSerialized]
    public UnityEvent<List<Spaceship>> OnSpaceshipUpdate;
    
    
    public void SpaceshipUpdate(List<Spaceship> newData)
    {
        spaceships = newData;
        OnSpaceshipUpdate?.Invoke(spaceships);
        
    }
}
