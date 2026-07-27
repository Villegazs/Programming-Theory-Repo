using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. Inheritance: Explosive target inherits the health and points from target
public class ExplosiveTarget : Target
{
    [SerializeField] private float explosionRadius = 5.0f;

    public override void OnHit()
    {
        Health -= 1;
        if (Health <= 0)
        {
            Explode();
            
            DestroyTarget();
        }
    }

    private void Explode()
    {
        Debug.Log("White explosive activated");

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObjects in colliders)
        {
            Target nearbyTarget = nearbyObjects.GetComponent<Target>();

            if (nearbyTarget != null && nearbyTarget != this)
            {
                nearbyTarget.OnHit();
            }
        }
    }
    
}
