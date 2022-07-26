using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Gradient healthGradient;
    [SerializeField] private Image healthFill;
    
    [SerializeField] private PlayerSO playerSO;
    
    private void Start() 
    {
        ChangeSliderValue(playerSO.currentHealth);
        healthFill.color = healthGradient.Evaluate(1f);
    }
    
    
    private void OnEnable()
    {
        playerSO.healthChangedEvent.AddListener(ChangeSliderValue);
    }
    
    private void OnDisable()
    {
        playerSO.healthChangedEvent.RemoveListener(ChangeSliderValue);
    }
    
    public void ChangeSliderValue(int amount)
    {
        healthSlider.value = ConvertIntToFloatDecimal(amount);
        healthFill.color = healthGradient.Evaluate(healthSlider.normalizedValue);
    }
    
    private float ConvertIntToFloatDecimal(int amount)
    {
        return (float)amount;
    }
}
