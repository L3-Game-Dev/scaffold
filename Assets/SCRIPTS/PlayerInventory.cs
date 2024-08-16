// PlayerInventory
// Handles the player's inventory
// Created by Dima Bethune 15/06

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerInventory : MonoBehaviour
{
    [Header("References")]
    [HideInInspector] public List<Weapon> heldWeapons = new List<Weapon>();
    public Weapon equippedWeapon;
    public GrenadeThrower grenadeThrower;
    [HideInInspector] public StarterAssetsInputs input;
    [HideInInspector] public PlayerStats playerStats;
    [HideInInspector] public UiHandler uiHandler;

    public int heldBandages;


    public int heldMedkits;

    public int heldCredits;

    private void Awake()
    {
        heldCredits = 0;

        // Set references
        input = GetComponent<StarterAssetsInputs>();
        playerStats = GetComponent<PlayerStats>();
        uiHandler = GameObject.Find("-- UI ELEMENTS --").GetComponent<UiHandler>();

        // Add every existing player weapon to inventory
        foreach (Transform w in transform.Find("PlayerCameraRoot"))
        {
            if (w.CompareTag("PlayerWeapon")) // Objects with 'PlayerWeapon' tag
            {
                heldWeapons.Add(w.gameObject.GetComponent<Weapon>()); // Add to inventory
            }
        }

        // If weapon inventory is not empty
        if (heldWeapons.Count != 0)
        {
            equippedWeapon = heldWeapons[0]; // Equip the first weapon
            uiHandler.UpdateInventoryUI(heldWeapons);
        }
        else // If it is empty
        {
            Debug.Log("No weapons found...");
        }

        UpdateEnabledWeapons();
    }

    private void Update()
    {
        if (!playerStats.isDead && GameStateHandler.gameState == "PLAYING")
        {
            EquipWeaponInput();
            uiHandler.UpdateAmmoNumberUI(heldWeapons);
        }
    }

    public void EquipWeaponInput()
    {
        if (input.equipWeapon1 && heldWeapons.Count >= 1)
            EquipWeapon(0);
        if (input.equipWeapon2 && heldWeapons.Count >= 2)
            EquipWeapon(1);
        if (input.equipWeapon3 && heldWeapons.Count >= 3)
            EquipWeapon(2);
        if (input.equipWeapon4 && heldWeapons.Count >= 4)
            EquipWeapon(3);
    }

    public void EquipWeapon(int i)
    {
        // Cancel previous weapon's reloading
        equippedWeapon.CancelInvoke("ReloadFinished");
        equippedWeapon.reloading = false;
        // Equip new weapon
        equippedWeapon = heldWeapons[i];
        UpdateEnabledWeapons();
    }

    public void UpdateEnabledWeapons()
    {
        // Disable all non-equipped weapons, enabled equipped weapon
        foreach (Weapon w in heldWeapons)
        {
            if (w != equippedWeapon)
                w.gameObject.SetActive(false);
            else
                w.gameObject.SetActive(true);
        }
    }

    public void UseMedkit()
    {
        heldMedkits--;
        playerStats.health += 100; 
    }

    public void UseBandage()
    {
        heldBandages--;
        playerStats.health += 50;
    }
}
