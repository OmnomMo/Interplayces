
using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;

        Debug.Log("Collision!");


        if ((hit.GetComponent<Health>() as Health) != null) {

            Debug.Log("damage!");

            var health = hit.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(10);
            }

        }

        Destroy(gameObject);
    }
}