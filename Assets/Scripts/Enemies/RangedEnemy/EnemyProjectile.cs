using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    private float lifeTime = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LifeTimeCountDown());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += -transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other) {
        // Player collision layer
        if(other.gameObject.layer == 6)
        {
            // Call player do damage method
            Destroy(gameObject);
            return;
        }
        // Ground collision layer
        if(other.gameObject.layer == 3)
        {
            Destroy(gameObject);
        }
    }
   
    IEnumerator LifeTimeCountDown()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
