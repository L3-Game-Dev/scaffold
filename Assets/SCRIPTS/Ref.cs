// GeneralReferences
// Stores references to all single-instance scripts // WIP
// Created by Dima Bethune 17/06

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public static class Ref
{
    public static GameObject player;
    public static PlayerStats playerStats;
    public static PlayerCombat playerCombat;
    public static PlayerInventory playerInventory;
    public static StarterAssetsInputs input;
    public static FirstPersonController fpsController;
    public static UiHandler uiHandler;

    /*
    private void Awake()
    {
        player = GameObject.Find("PlayerCapsule");
        playerStats = player.GetComponent<PlayerStats>();
        playerCombat = player.GetComponent<PlayerCombat>();
        playerInventory = player.GetComponent<PlayerInventory>();
        input = player.GetComponent<StarterAssetsInputs>();
        fpsController = player.GetComponent<FirstPersonController>();
        uiHandler = GameObject.Find("-- UI ELEMENTS --").GetComponent<UiHandler>();
    }
    */
}
