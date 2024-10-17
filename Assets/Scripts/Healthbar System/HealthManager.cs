using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    public static UnityEvent OnDamage, OnHeal;

    [SerializeField]
    private float MAXHEALTH;

    public float health;

    private void Start()
    {
        if (MAXHEALTH <= 0)
        {
            Debug.Log("Health must be > 0, please set in inspector!");
        }

        health = MAXHEALTH;
    }

    public void DealDmg(float dmg)
    {
        health -= dmg;
        ClampHealth();

        OnDamage.Invoke();

        if (health == 0.0f)
        {
            //run death code here
        }

    }

    public void HealDmg(float heal)
    {
        health += heal;
        ClampHealth();

        OnHeal.Invoke();
    }

    private void ClampHealth()
    {
        Mathf.Clamp(health, 0.0f, MAXHEALTH);
    }
}
