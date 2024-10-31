using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    private static HealthManager _instance;
    public static HealthManager Instance { get { return _instance; } }

    public static UnityEvent OnDamage, OnHeal;

    [SerializeField]
    private float MAXHEALTH;

    public float health;

    private float slowCooldown = 1.0f, medCooldown = 0.6f, fastCooldown = 0.3f;

    public enum HealthStatus 
    {
        Full,
        Mid,
        Low,
        Dead
    }

    [SerializeField] private HealthStatus status;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
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
        DetermineHealthState();

        //OnDamage.Invoke();
    }

    public void HealDmg(float heal)
    {
        health += heal;
        ClampHealth();
        DetermineHealthState();

        //OnHeal.Invoke();
    }

    private void ClampHealth()
    {
        Mathf.Clamp(health, 0.0f, MAXHEALTH);
    }


    private void DetermineHealthState()
    {
        var ratio = health / MAXHEALTH;
        if (health <= 0.0f)
        {
            status = HealthStatus.Dead;
        }
        else if (ratio < 0.8f && ratio > 0.3f)
        {
            status = HealthStatus.Mid;
        }
        else if(ratio >= 1.0f && ratio <= 0.8f)
        {
            status = HealthStatus.Full;
        }
        else
        {
            status = HealthStatus.Low;
        }

        HealthToCooldowns();
    }

    //assuming health is handled in "chunks"
    private void HealthToCooldowns()
    {
        switch (status)
        {
            case HealthStatus.Full:
                PlayerCooldowns.BLOCK_WINDUP = slowCooldown;
                PlayerCooldowns.DODGE_COOLDOWN = slowCooldown;
                PlayerCooldowns.SWING_CANCELWINDOW = fastCooldown;
                PlayerCooldowns.SWING_DURATION = 1 / slowCooldown;
                WeaponScaling.Instance.ScaleWeapon(1.0f);
                break;

            case HealthStatus.Mid:
                PlayerCooldowns.BLOCK_WINDUP = medCooldown;
                PlayerCooldowns.DODGE_COOLDOWN = medCooldown;
                PlayerCooldowns.SWING_CANCELWINDOW = medCooldown;
                PlayerCooldowns.SWING_DURATION = 1 / medCooldown;
                WeaponScaling.Instance.ScaleWeapon(0.5f);
                break;

            case HealthStatus.Low:
                PlayerCooldowns.BLOCK_WINDUP = fastCooldown;
                PlayerCooldowns.DODGE_COOLDOWN = fastCooldown;
                PlayerCooldowns.SWING_CANCELWINDOW = slowCooldown;
                PlayerCooldowns.SWING_DURATION = 1 / fastCooldown;
                WeaponScaling.Instance.ScaleWeapon(0.2f);
                break;

            case HealthStatus.Dead:
                //run death code here
                break;

            default:
                break;
        }
    }
}
