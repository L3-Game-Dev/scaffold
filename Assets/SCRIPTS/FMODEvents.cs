// FMODEvents
// Stores references to general FMOD events
// Created by Dima Bethune 05/07

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("SFX")]
    [field: SerializeField] public EventReference playerFootstepsMetal;

    // Singleton functionality
    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one AudioManager found");
        }
        instance = this;
    }
}
