using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 50f;
    [SerializeField] GameObject viewDamageAmount;

    public void TakeDamage(float firePower)
    {
        health -= firePower;

        GameObject DamageAmountInstantiated = Instantiate(viewDamageAmount, gameObject.transform);
        Destroy(DamageAmountInstantiated, 1f);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
