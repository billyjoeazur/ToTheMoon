using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "EnemySO", menuName = "ToTheMoon/EnemySO", order = 0)]
public class EnemySO : ScriptableObject
{
    public GameObject enemyModel;
    public float maxHealth;
    public int coinToDrop;
    public int diamondToDrop;
    public int scoreToAdd;
    public int expToAdd;
    
    private void OnEnable() 
    {
        
    }
}
