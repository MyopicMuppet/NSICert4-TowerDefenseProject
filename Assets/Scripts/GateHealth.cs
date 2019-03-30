using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int health = 0; // Note (Manny): You weren't displaying health.
    // Use this for initialization
    void Start ()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
