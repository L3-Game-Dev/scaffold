// Grenade
// Handles grenade functionality
// Created by Dima Bethune 24/06

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [Header("Variables")]
    public float damage;
    public float detonationTime;
    public bool detonateOnImpact;
    public float throwSpeed;
    public float detonationRadius;
    public bool affectsPlayer;
    public GameObject grenadeEffect;

    [HideInInspector] public GrenadeThrower thrower;
    [HideInInspector] public PlayerCombat playerCombat;
    [HideInInspector] public PlayerStats playerStats;

    public GameObject[] enemiesInRange;

    private void Awake()
    {
        // Set references
        playerCombat = GameObject.Find("PlayerCharacter").transform.Find("PlayerCapsule").GetComponent<PlayerCombat>();
        playerStats = GameObject.Find("PlayerCharacter").transform.Find("PlayerCapsule").GetComponent<PlayerStats>();
        thrower = playerCombat.playerInventory.grenadeThrower;
    }

    private void Start()
    {
        Invoke("DetonateGrenade", detonationTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (detonateOnImpact)
        {
            DetonateGrenade();
        }
    }

    private void DetonateGrenade()
    {
        // Get all enemies
        enemiesInRange = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemiesInRange)
        {
            // Damage enemies in radius
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance <= detonationRadius)
            {
                if (enemy.GetComponent<EnemyStats>())
                    enemy.GetComponent<EnemyStats>().ModifyHealth('-', damage);
                else if (enemy.GetComponent<MinibossStats>())
                    enemy.GetComponent<MinibossStats>().ModifyHealth('-', damage);
            }
        }

        // Damage player if in radius
        if (affectsPlayer && (Vector3.Distance(playerCombat.transform.position, transform.position) <= detonationRadius))
        {
            playerStats.ModifyHealth('-', damage);
        }

        // Spawn grenade effect
        Instantiate(grenadeEffect, transform.position, transform.rotation, GameObject.Find("PlayerProjectiles").transform);

        DestroyGrenade();
    }

    private void DestroyGrenade()
    {
        Destroy(gameObject);
    }

#if UNITY_EDITOR

    // Show grenade radius in editor
    private void OnDrawGizmos()
    {
        Color radiusColor = Color.red;
        radiusColor.a = 0.2f;
        Gizmos.color = radiusColor;
        Gizmos.DrawSphere(transform.position, detonationRadius);
    }

#endif

}