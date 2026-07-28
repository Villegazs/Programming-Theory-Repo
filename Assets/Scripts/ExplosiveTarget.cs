using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. Inheritance: Explosive target inherits the health and points from target
public class ExplosiveTarget : Target
{
    [SerializeField] private float explosionRadius = 5.0f;
    private Collider[] hitColliders;
    private const int maxColliders = 8;

    private void Awake()
    {
        hitColliders = new Collider[maxColliders];
    }

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

        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, explosionRadius, hitColliders);

        for (int i = 0; i < numColliders; i++)
        {
            Collider c = hitColliders[i];
            
            if(c.gameObject == this.gameObject) continue;
            
            Target nearbyTarget = c.GetComponent<Target>();
            
            if (nearbyTarget != null && nearbyTarget != this)
            {
                nearbyTarget.OnHit();
            }
            
        }
        
    }
    
}
