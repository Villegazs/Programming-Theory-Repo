using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float pointValue =10;
    // Encapsulation
    public float PointValue { get => pointValue;}
    [SerializeField] private float health = 1;

    protected float Health
    {
        get => health;
        set { health = Mathf.Max(value, 0f); }
    }
    [SerializeField] protected ParticleSystem deathEffect;
    [SerializeField] protected AudioSource deathSound;

    public virtual void OnHit()
    {
        health --;
        if (health <= 0)
        {
            DestroyTarget();
        }
    }

    public void DestroyTarget()
    {
        deathEffect.Play();
        deathSound.Play();
        Destroy(gameObject);
        
    }
    
}
