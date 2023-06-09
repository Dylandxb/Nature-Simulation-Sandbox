using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public enum Stats
{
    Stamina,
    Hunger,
    Strength,
    Thirst,

}

//Lerp these values down over the course of the day
//Use event manager and day/night cycle
public class PlayerStats : MonoBehaviour
{
    ConsumableController consumableController;
    //Use statics
    [Header("Base Values")]
    [SerializeField] public int baseStaminaValue = 0;
    [SerializeField] public int baseHungerValue = 0;
    [SerializeField] public int baseStrengthValue = 0;
    [SerializeField] public int baseThirstValue = 0;
    [Header("Current Values")]
    [SerializeField] public float currentStaminaValue = 1;
    [SerializeField] public float currentHungerValue = 1;
    [SerializeField] public float currentStrengthValue = 1;
    [SerializeField] public float currentThirstValue = 1;
    [Header("Max Values")]
    [SerializeField] public int maxStamina = 100;
    [SerializeField] public int maxHunger = 100;
    [SerializeField] public int maxStrength = 100;
    [SerializeField] public int maxThirst = 100;
    //Use this to get players current position in world and save it on next load
    [Header("Player Position")]
    [SerializeField] private Vector3 playerPosition;
    [SerializeField] private Vector3 startPosition = new Vector3(0,0,0);
    [SerializeField] private GameObject player;
    public static float percentage;
    public float decreaseSpeed;
    public bool saved;
    public bool loaded;
    public static PlayerStats instance { get; private set; }



    private void Awake()
    {
        player = GameObject.Find("Player");
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        currentStaminaValue = maxStamina;
        currentHungerValue = maxHunger;
        currentStrengthValue = maxStrength;
        currentThirstValue = maxThirst;
    }

    void Start()
    {
        //Make it so when play enter objects of consumable stats increase by value in inspector
        consumableController = GetComponent<ConsumableController>();
        LoadSavedStats();
        playerPosition = player.transform.position;
        StartCoroutine(WaitBeforeDecrease());
        EventManager.instance.StartSleep += SaveCurrentStats;
        //Restores stats in sleep
        //EventManager.instance.StartSleep += ResetCurrentStats;
        EventManager.instance.EndSleep += LoadSavedStats;
    }

    private void OnDisable()
    {
        EventManager.instance.StartSleep -= SaveCurrentStats;
        //EventManager.instance.StartSleep -= ResetCurrentStats;
        EventManager.instance.EndSleep -= LoadSavedStats;
    }
    private IEnumerator WaitBeforeDecrease()
    {
        yield return new WaitForSeconds(10.0f);
        //Decrease specificied stats by a value over time
        InvokeRepeating("DecreaseHunger", 1.0f, 10.0f);
        InvokeRepeating("DecreaseThirst", 1.0f, 5.0f);
        yield return null;
    }
    private void Update()
    {
        playerPosition = player.transform.position;

        //UpdateSpeed();

        CapHunger();
        CapThirst();
        CapStamina();
        CapStrength();

        if (Input.GetKeyDown(KeyCode.H))
        {
            ResetCurrentStats();
        }
        if (saved == true)
        {
            saved = false;
        }
        if (loaded == true)
        {
            loaded = false;
        }
    }

    public void SaveCurrentStats()
    {
        PlayerPrefs.SetFloat(Stats.Stamina.ToString(), currentStaminaValue);
        PlayerPrefs.SetFloat(Stats.Strength.ToString(), currentStrengthValue);
        PlayerPrefs.SetFloat(Stats.Thirst.ToString(), currentThirstValue);
        PlayerPrefs.SetFloat(Stats.Hunger.ToString(), currentHungerValue);
        PlayerPrefs.SetFloat("playerPosX", playerPosition.x);
        PlayerPrefs.SetFloat("playerPosY", playerPosition.y);
        PlayerPrefs.SetFloat("playerPosZ", playerPosition.z);
        PlayerPrefs.Save();
        saved = true;
    }

    public void LoadSavedStats()
    {
        currentStaminaValue = PlayerPrefs.GetFloat(Stats.Stamina.ToString(), currentStaminaValue);
        currentStrengthValue = PlayerPrefs.GetFloat(Stats.Strength.ToString(), currentStrengthValue);
        currentThirstValue = PlayerPrefs.GetFloat(Stats.Thirst.ToString(), currentThirstValue);
        currentHungerValue = PlayerPrefs.GetFloat(Stats.Hunger.ToString(), currentHungerValue);
        float playerPosX = PlayerPrefs.GetFloat("playerPosX");
        float playerPosY = PlayerPrefs.GetFloat("playerPosY");
        float playerPosZ = PlayerPrefs.GetFloat("playerPosZ");
        Vector3 playerPos = new Vector3(playerPosX, playerPosY, playerPosZ);
        player.transform.position = playerPos;
        loaded = true;
    }

    public void ResetCurrentStats()
    {
        //Resets the current value between 0-100 back to their max
        currentStaminaValue = maxStamina;
        currentStrengthValue = maxStrength;
        currentThirstValue = maxThirst;
        currentHungerValue = maxHunger;
    }

    public void ResetToBase()
    {
        //Resets the current values all to 0 for debug
        currentStaminaValue = baseStaminaValue;
        currentStrengthValue = baseStrengthValue;
        currentThirstValue = baseThirstValue;
        currentHungerValue = baseHungerValue;

    }

    public void ResetPlayerPos()
    {
        //Set player back to original position in world (0,0,0)
        playerPosition.x = startPosition.x;
        playerPosition.y = startPosition.y;
        playerPosition.z = startPosition.z;
        playerPosition = startPosition;
        Vector3 origin = new Vector3(0, 1.979f, 0);
        player.transform.position = origin;
        transform.position = Vector3.MoveTowards(player.transform.position, origin, 0.5f * Time.deltaTime);
    }

    public void LerpHunger()
    {
        //eg every second sprinting is 1 tick off 100 %
        //float target = 0;
        //float currentVal;
        ////Use get set
        //float timeSprinting = 10.0f;
        //if(currentHungerValue > 0)
        //{
        //    //Mathf.Lerp(currentHungerValue, 0, 0.1f * Time.deltaTime);
        //    currentVal =  Mathf.MoveTowards(currentHungerValue, target, 5.0f * Time.deltaTime);

        //    Debug.Log(currentVal);
        //}
        //while (timeSprinting < 20.0f)
        //{
        //    currentHungerValue -= 2;
        //    timeSprinting += 1.0f;
        //}
        //float currentVal =Mathf.Lerp(currentHungerValue, 0, 0.5f * Time.deltaTime);
        currentHungerValue -= decreaseSpeed * Time.deltaTime;

    }
    public void RangeSpeed()
    {
        //Lerp hunger down when sprinting
        if (currentHungerValue >= 75)
        {
            PlayerController.instance.maximumSpeed = 4.0f;
        }
        else if (currentHungerValue <75 && currentHungerValue >=50)
        {
            PlayerController.instance.maximumSpeed = 3.0f;
        }
        else if (currentHungerValue <50 && currentHungerValue >= 25)
        {
            PlayerController.instance.maximumSpeed = 2.0f;
        }
        else if (currentHungerValue <25)
        {
            PlayerController.instance.maximumSpeed = 1.0f;
        }

    }
    public void RangeJumpHeight()
    {
     //Lower strength lower jump height
        if(currentStrengthValue>=75)
        {
            PlayerController.instance.jumpHeight = 1.25f;
        }
        else if (currentStrengthValue < 75 && currentStrengthValue >=50)
        {
            PlayerController.instance.jumpHeight = 1.05f;
        }
        else if(currentStrengthValue < 50 && currentStrengthValue >=25)
        {
            PlayerController.instance.jumpHeight = 0.85f;
        }
        else if(currentStrengthValue < 25)
        {
            PlayerController.instance.jumpHeight = 0.65f;
        }

    }

    public void DecreaseStamina()
    {
        StartCoroutine(LowerStaminaWithAltitude(0.02f, decreaseSpeed));
        //Stamina decay logic -> Lower stamina = longer sleep time
        //Stamina regenerates with sleep and eating/drinking
        //When stamina is 0 player needs to sleep to regenerate their stamina bar and continue exploration
        
    }

    private IEnumerator LowerStaminaWithAltitude(float stamina, float rate)
    {
        //Temp value to store the new stamina value
        float staminaValue = currentStaminaValue - stamina;
        while (currentStaminaValue > staminaValue)
        {
            //While the current stamina is greater than temp value begin the decrease
            currentStaminaValue -= rate * Time.deltaTime;
            yield return null; 
        }
        //After iterating set the current stamina value to the new stamina value
        currentStaminaValue = staminaValue;
        yield break;
    }
    public void DecreaseHunger()
    {
        //less hungner -> Higher probability of animals and food spawning
        if (currentHungerValue >0)
        {
            currentHungerValue -= 1;
        }
    }

    public void DecreaseThirst()
    {
        //Less thirst -> Higher probability of water source spawning,
        if (currentThirstValue>0)
        {
            currentThirstValue -= 1;
        }
    }
    public void DecreaseStrength()
    {
        currentStrengthValue -= 5;
        if(currentStrengthValue < 0)
        {
            currentStrengthValue = 0;
        }
    }

    //Capped values
    public void CapStamina()
    {
        //Stamina
        if (currentStaminaValue > maxStamina)
        {
            currentStaminaValue = maxStamina;
        }
        else if (currentStaminaValue <= baseStaminaValue)
        {
            currentStaminaValue = baseStaminaValue;
        }
    }

    public void CapHunger()
    {
        //Hunger
        if (currentHungerValue > maxHunger)
        {
            currentHungerValue = maxHunger;
        }
        else if (currentHungerValue <= baseHungerValue)
        {
            currentHungerValue = baseHungerValue;
        }
    }

    public void CapThirst()
    {
        //Thirst
        if (currentThirstValue > maxThirst)
        {
            currentThirstValue = maxThirst;
        }
        else if (currentThirstValue <= baseThirstValue)
        {
            currentThirstValue = baseThirstValue;
        }
    }

    public void CapStrength()
    {
        if (currentStrengthValue > maxStrength)
        {
            currentStrengthValue = maxStrength;
        }
        else if (currentStrengthValue <= baseStrengthValue)
        {
            currentStrengthValue = baseStrengthValue;
        }
    }
}
