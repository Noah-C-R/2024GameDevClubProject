using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtboxInteractions : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 7)
        {
            HealthManager.Instance.DealDmg(3);
        }
    }
}
