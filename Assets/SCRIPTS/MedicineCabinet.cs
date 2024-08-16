// Medicine Cabinet
// Handles all functionality for the medicine cabinet
// Created by Quinton Mortell 29/07/24

using UnityEngine;

public class MedicineCabinet : MonoBehaviour
{
    [HideInInspector] public UiHandler uiHandler;
    [HideInInspector] public PlayerInteract interact;
    [HideInInspector] public PlayerInventory inventory;
    [HideInInspector] public PlayerStats playerStats;

    int maxHealthUpdateAmount = 50;
    int healthRegenSpeedUpgradeAmount = 15;

    public bool cabinetEnabled;
    
    private void Awake()
    {
        uiHandler = GameObject.Find("-- UI ELEMENTS --").GetComponent<UiHandler>();
        interact = GameObject.Find("PlayerCapsule").GetComponent<PlayerInteract>();
        inventory = GameObject.Find("PlayerCapsule").GetComponent<PlayerInventory>();
        playerStats = GameObject.Find("PlayerCapsule").GetComponent<PlayerStats>();
    }

    private void Update()
    {
        MedicineCabinetPanel();
    }

    public void MedicineCabinetPanel()
    {
        if (interact.lookingAt == gameObject && GameStateHandler.gameState == "PLAYING")
        {
            if (Input.GetKeyDown(interact.keyBind)) // If player presses interact keyBind
            {
                // Open console panel
                uiHandler.ToggleUI(true, uiHandler.medicineCabinetPanel);
                // Pause the game
                GameStateHandler.Pause();
            }
        }
    }

    public void BuyBandage()
    {
        inventory.heldBandages++;
        inventory.heldCredits -= 25;
    }

    public void BuyMedkits()
    {
        inventory.heldMedkits++;
        inventory.heldCredits -= 50;
    }

    public void BuyVitalityCore()
    {
        playerStats.maxHealth += maxHealthUpdateAmount;
        inventory.heldCredits -= 200;
    }

    public void BuyRegenerationModule()
    {
        playerStats.healthRegenSpeed += healthRegenSpeedUpgradeAmount;
        inventory.heldCredits -= 200;
    }
}
