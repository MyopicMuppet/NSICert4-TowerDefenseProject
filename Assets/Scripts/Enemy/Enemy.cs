using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int damage = 10;
    public float attackRate = 5f;
    public float attackRange = 5f;

    private int health = 0;

    void Start()
    {
        // Set health to whatever maxHealth is at start
        health = maxHealth;
    }

    // Call this function to make Enemy take damage
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Selector.changeMoney(100);

            Destroy(gameObject);
        }
    }
    // Aims at a given enemy every frame
    public virtual void Aim(GateHealth g)
    {
        print("Base Enemy is aiming at '" + g.name + "'");
    }
    // Attacks at a given enemy only when 'attacking'
    public virtual void Attack(GateHealth g)
    {
        print("Base Enemy is attacking '" + g.name + "'");
    }
}
