using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "ToTheMoon/PlayerSO", order = 0)]
public class PlayerSO : ScriptableObject
{
    public Player player;
    public int highestScore;
    [NonSerialized]
    public UnityEvent<Player> OnPlayerDataUpdate;
    
    private void OnEnable() 
    {
        if (OnPlayerDataUpdate == null)
        {
            OnPlayerDataUpdate = new UnityEvent<Player>();
        }
    }
    
    public void PlayerDataUpdate(Player newData)
    {
        player = newData;
        OnPlayerDataUpdate?.Invoke(player);
    }
}
