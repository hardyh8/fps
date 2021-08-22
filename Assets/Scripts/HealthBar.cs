using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    private int MaxHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = 100;
        slider = GetComponent<Slider>();
        SetSlider(MaxHealth);
    }

    public void SetSlider(int health)
    {
        slider.value = health;
    }

    public void SetMaxHealth(int maxHealth)
    {
        this.MaxHealth = maxHealth;
        slider.maxValue = maxHealth;
    }

    public void Reset()
    {
        slider.value = MaxHealth;
        slider.maxValue = MaxHealth;
    }
}
