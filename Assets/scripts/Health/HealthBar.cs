using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public TextMeshProUGUI health;
    public HealthSystem healthSystem;
    public void Start()
    {
        
    }
    public void Update()
    {
        
        Updatehealth();
    }
    public void Updatehealth()
    {
        health.text = healthSystem.currentHealth.ToString() + "/10" ;
    }
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(float health)
    {
        slider.value = health;
    }
}