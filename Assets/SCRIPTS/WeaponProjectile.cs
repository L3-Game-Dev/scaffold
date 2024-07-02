// WeaponProjectile
// Handles spawning & functionality of weapon projectiles
// Created by Dima Bethune 05/06

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProjectile : MonoBehaviour
{
    [Header("Variables")]
    [HideInInspector] public float damage;
    public Weapon weapon;
    [HideInInspector] public PlayerCombat playerCombat;
    public float decayTime;

    private void Awake()
    {
        // Set references
        playerCombat = GameObject.Find("PlayerCharacter").transform.Find("PlayerCapsule").GetComponent<PlayerCombat>();
    }

    private void Start()
    {
        // Set variable values
        damage = weapon.GetComponent<Weapon>().damage * weapon.GetComponent<Weapon>().damageMultiplier;

        // Destroy after decayTime
        Invoke("DestroyProjectile", decayTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (gameObject.CompareTag("PlayerProjectile"))
        {
            if (collision.CompareTag("Enemy"))
            {
                Debug.Log("PlayerProjectile HIT Enemy");

                if (collision.GetComponent<EnemyStats>())
                {
                    collision.GetComponent<EnemyStats>().ModifyHealth('-', damage);
                }
                else if (collision.GetComponent<MinibossStats>())
                {
                    collision.GetComponent<MinibossStats>().ModifyHealth('-', damage);
                }
            }
            else
            {
                Debug.Log("PlayerProjectile HIT Something");
            }
        }
        else if (gameObject.CompareTag("EnemyProjectile"))
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerStats>().ModifyHealth('-', damage);
            }
            else
            {
                Debug.Log("EnemyProjectile HIT Something");
            }
        }
        else
        {
            Debug.Log("NON-TAGGED PROJECTILE COLLISION");
        }

        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
