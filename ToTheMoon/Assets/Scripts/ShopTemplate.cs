using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTemplate : MonoBehaviour
{
    public Text titleTxt;
    public Text descriptionTxt;
    public Text currentPriceTxt;
    
    void Update()
    {
        transform.GetChild(1).FindChild(titleTxt.text).gameObject.SetActive(true);
    }
}




