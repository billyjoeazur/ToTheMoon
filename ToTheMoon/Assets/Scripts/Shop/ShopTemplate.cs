using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTemplate : MonoBehaviour
{
    public Text titleTxt;
    public Text descriptionTxt;
    public Text levelTxt;
    public Text currentPriceTxt;
    public ShopMan man;
    
    void Update()
    {
        transform.GetChild(1).Find(titleTxt.text).gameObject.SetActive(true); //show the models base on its title text
    }
    
    void OnEnable()
    {
        man = FindObjectOfType<ShopMan>();
        man.OnHeaderButtonPress += HideAllModels;
    }
    
    void OnDisable()
    {
        man.OnHeaderButtonPress -= HideAllModels;
    }
    
    void HideAllModels(string tab)
    {
        for (int i = 0; i < transform.GetChild(1).childCount; i++)
        {
            transform.GetChild(1).GetChild(i).gameObject.SetActive(false);
        }
    }
}




