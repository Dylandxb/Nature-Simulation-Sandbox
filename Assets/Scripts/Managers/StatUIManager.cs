using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatUIManager : MonoBehaviour
{
    public TextMeshProUGUI staminaValue;
    public TextMeshProUGUI thirstValue;
    public TextMeshProUGUI hungerValue;
    public TextMeshProUGUI strengthValue;

    [SerializeField] private Image hungerBar;
    [SerializeField] private Image thirstBar;
    [SerializeField] private Image strengthBar;
    [SerializeField] private Image staminaBar;

    void Start()
    {
        //Initializes the current stat values and sets them in the UI
        staminaValue.text = PlayerStats.instance.currentStaminaValue.ToString("F0") + "%";
        thirstValue.text = PlayerStats.instance.currentThirstValue.ToString() + "%";
        hungerValue.text = PlayerStats.instance.currentHungerValue.ToString("F0") + "%";
        strengthValue.text = PlayerStats.instance.currentStrengthValue.ToString() + "%";
        UpdateHungerBar(PlayerStats.instance.maxHunger, PlayerStats.instance.currentHungerValue);
        UpdateThirstBar(PlayerStats.instance.maxThirst, PlayerStats.instance.currentThirstValue);
        UpdateStrengthBar(PlayerStats.instance.maxStrength, PlayerStats.instance.currentStrengthValue);
        UpdateStaminaBar(PlayerStats.instance.maxStamina, PlayerStats.instance.currentStaminaValue);
    }

    void Update()
    {
        //Any change to a stat is checked every frame and updates the UI value
        staminaValue.text = PlayerStats.instance.currentStaminaValue.ToString("F0") + "%";
        thirstValue.text = PlayerStats.instance.currentThirstValue.ToString() + "%";
        hungerValue.text = PlayerStats.instance.currentHungerValue.ToString("F0") + "%";
        strengthValue.text = PlayerStats.instance.currentStrengthValue.ToString() + "%";
        UpdateHungerBar(PlayerStats.instance.maxHunger, PlayerStats.instance.currentHungerValue);
        UpdateThirstBar(PlayerStats.instance.maxThirst, PlayerStats.instance.currentThirstValue);
        UpdateStrengthBar(PlayerStats.instance.maxStrength, PlayerStats.instance.currentStrengthValue);
        UpdateStaminaBar(PlayerStats.instance.maxStamina, PlayerStats.instance.currentStaminaValue);
    }
    //Sets UI bar fill amount values to percentages of the total
    public void UpdateHungerBar(float maxHunger, float currentHunger)
    {
        hungerBar.fillAmount = currentHunger / maxHunger;
    }

    public void UpdateThirstBar(float maxThirst, float currentThirst)
    {
        thirstBar.fillAmount = currentThirst / maxThirst;
    }

    public void UpdateStrengthBar(float maxStrength, float currentStrength)
    {
        strengthBar.fillAmount = currentStrength / maxStrength;
    }

    public void UpdateStaminaBar(float maxStamina, float currentStamina)
    {
        staminaBar.fillAmount = currentStamina / maxStamina;
    }
}

