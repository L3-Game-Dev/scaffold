// DoorControl
// Handles opening tile doors through console panel UI
// Created by Dima Bethune 29/05

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.UI;

public class DoorControl : MonoBehaviour
{
    [HideInInspector] public UiHandler uiHandler;
    [HideInInspector] public PlayerInteract interact;

    public bool doorControlEnabled;

    private void Awake()
    {
        uiHandler = GameObject.Find("-- UI ELEMENTS --").GetComponent<UiHandler>();
        interact = GameObject.Find("PlayerCapsule").GetComponent<PlayerInteract>();
    }

    private void Update()
    {
        DoorControlPanel();
    }

    public void DoorControlPanel()
    {
        if (interact.lookingAt == gameObject && GameStateHandler.gameState == "PLAYING")
        {
            if (Input.GetKeyDown(interact.keyBind)) // If player presses interact keyBind
            {
                // Open console panel
                uiHandler.ToggleUI(true, uiHandler.doorConsolePanel);
                // Pause the game
                GameStateHandler.Pause();
            }
        }
    }

    public void OpenDoor(GameObject door)
    {
        if (doorControlEnabled)
        {
            // Set 'DoorParts' reference
            Transform doorParts = door.transform.Find("DoorParts");

            // Disable every doorPart from the door
            foreach (Transform doorPart in doorParts)
            {
                doorPart.gameObject.SetActive(false);
            }
        }
    }
}
