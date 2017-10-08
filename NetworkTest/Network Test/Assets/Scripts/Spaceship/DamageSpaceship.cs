using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSpaceship : MonoBehaviour
{

    public float damage;
    public float maxDamage;
    public GameObject impactParticleEffect;

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


                if (damageDealt > 0.5f)
                {
                    GameObject delayedExplosion = GameObject.Instantiate(impactParticleEffect);
                    delayedExplosion.transform.SetParent(SpaceshipGameplay.Instance.transform);
                    delayedExplosion.transform.localPosition = Vector3.zero;

                    ParticleSystem.MainModule impactMain = delayedExplosion.GetComponent<ParticleSystem>().main;

                    impactMain.startSpeed = damageDealt * 25;

                    ParticleSystem.EmissionModule impactEmission = delayedExplosion.GetComponent<ParticleSystem>().emission;

                    short nBursts = Convert.ToInt16(damageDealt * 3);

                    if (nBursts > 12)
                    {
                        nBursts = 12;
                    }

                    impactEmission.SetBursts(
                        new ParticleSystem.Burst[] { new ParticleSystem.Burst(0.0f, nBursts) }
                        );

                    //delayedExplosion.GetComponent<ParticleSystem>().main = impactMain;

                }

            }
        }

    }
}
