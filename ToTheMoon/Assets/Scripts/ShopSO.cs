using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ShopSO", menuName = "ToTheMoon/ShopSO", order = 1)]
public class ShopSO : ScriptableObject
{
    public List<Spaceship> spaceships = new List<Spaceship>();
    public List<Essentials> essentials = new List<Essentials>();
    public List<Extras> extras = new List<Extras>();
    
    public int _coins;
	public int _diamonds;
    
    [NonSerialized] public UnityEvent<int> OnCurrencyUpdate;
    [NonSerialized] public UnityEvent<List<Spaceship>> OnSpaceshipUpdate;
    [NonSerialized] public UnityEvent<List<Essentials>> OnEssentialUpdate;
    [NonSerialized] public UnityEvent<List<Extras>> OnExtraUpdate;
    
    
    public void SpaceshipUpdate(List<Spaceship> newData)
    {
        spaceships = newData;
        OnSpaceshipUpdate?.Invoke(spaceships);
        
    }
    
    public void EssentialUpdate(List<Essentials> newData)
    {
        essentials = newData;
        OnEssentialUpdate?.Invoke(essentials);
        
    }
    
    public void ExtraUpdate(List<Extras> newData)
    {
        extras = newData;
        OnExtraUpdate?.Invoke(extras);
        
    }
}
