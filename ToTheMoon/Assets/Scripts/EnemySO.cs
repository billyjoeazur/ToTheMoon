using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "ToTheMoon/EnemySO", order = 0)]
public class EnemySO : ScriptableObject
{
    public GameObject enemyModel;
    public GameObject coinModel;
    public GameObject diamondModel;
    public int coinToDrop;
    public int diamondToDrop;
}
