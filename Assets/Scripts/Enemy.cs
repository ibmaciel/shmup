using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool hasSpawn;
    private Move moveScript;
    private Weapon [] weapons;
    private Collider2D colliderComponent;
    private SpriteRenderer rendererComponent;

    void Awake()
    {        
        weapons = GetComponentsInChildren<Weapon>();

        moveScript = GetComponent<Move>();

        colliderComponent = GetComponent<Collider2D>();

        rendererComponent = GetComponent<SpriteRenderer>();
    }
        
    void Start()
    {
        hasSpawn = false;

        colliderComponent.enabled = false;
        moveScript.enabled = false;
        foreach (Weapon weapon in weapons)
        {
            weapon.enabled = false;
        }
    }

    void Update()
    {        
        if (hasSpawn == false)
        {
            if (rendererComponent.IsVisibleFrom(Camera.main))
            {
                Spawn();
            }
        }
        else
        {            
            foreach (Weapon weapon in weapons)
            {
                if (weapon != null && weapon.enabled && weapon.CanAttack)
                {
                    weapon.Attack(true);
                }
            }
                        
            if (rendererComponent.IsVisibleFrom(Camera.main) == false)
            {
                Destroy(gameObject);
            }
        }
    }
        
    private void Spawn()
    {
        hasSpawn = true;

        colliderComponent.enabled = true;
        moveScript.enabled = true;
        foreach (Weapon weapon in weapons)
        {
            weapon.enabled = true;
        }
    }
}
