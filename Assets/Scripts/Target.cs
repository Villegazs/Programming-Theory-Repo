using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float pointValue =10;
    // ENCAPSULATION
    public float PointValue { get => pointValue;}
    [SerializeField] private float health = 1;

    protected float Health
    {
        get => health;
        set { health = Mathf.Max(value, 0f); }
    }

    [SerializeField] private float speed = 3f;
    [SerializeField] private int damageToPlayer = 1;
    
    private Transform playerTransform;
    
    [SerializeField] protected ParticleSystem deathEffect;
    [SerializeField] protected AudioSource deathSound;
    
    private Rigidbody rb;
    
    protected bool isDead = false;
    
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    private void FixedUpdate()
    {
        MoveTowardsPlayer();
        LookAtPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if (playerTransform != null)
        {
            Vector3 direction = (playerTransform.position - rb.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
            
            //transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));
        }
    }

    private void LookAtPlayer()
    {
        if (playerTransform != null)
        {
            Vector3 direction = (playerTransform.position - rb.position);
            direction.y = 0f;
            
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            
            rb.MoveRotation(targetRotation);
        }
    }
    
    public virtual void OnHit()
    {
        OnHit(1f);
    }

    // POLYMORPHISM
    public virtual void OnHit(float damage)
    {
        if(isDead) return;
        
        Health -= damage;
        if (Health <= 0)
        {
            isDead = true;
            DestroyTarget();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isDead) return;
        
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(collision.gameObject.name);
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            if (player != null)
            {
                player.TakeDamage(damageToPlayer);
                
                DestroyTarget();
            }
        }
    }

    // ABSTRACTION
    public virtual void DestroyTarget()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(pointValue);
        }
        if(deathEffect != null)
            deathEffect.Play();
        
        if (deathSound != null)
            deathSound.Play();
        
        Destroy(gameObject);
    }
    
}
