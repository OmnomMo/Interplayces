using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSpaceship : MonoBehaviour
{

    public float damage;
    public float maxDamage;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (GameState.Instance.isPlayerCaptain())
        {

            if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player") || collision.collider.gameObject.layer == LayerMask.NameToLayer("Shield"))
            {
                float damageDealt = damage * Vector3.Magnitude(collision.relativeVelocity);

                //Debug.Log("Damage Dealt: " + damageDealt);

                if (damageDealt > maxDamage)
                {
                    damageDealt = maxDamage;
                }
                SpaceshipGameplay.Instance.DealShieldDamage(damageDealt);
            }
        }

    }
}
