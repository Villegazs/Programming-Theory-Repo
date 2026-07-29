using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // ENCAPSULATION
    [SerializeField] private float speed = 15f;
    [SerializeField] private float lifetime = 3f; // Destroy 
    
    private Vector3 moveDirection;

    // ABSTRACTION
    public void Initialize(Vector3 direction)
    {
        moveDirection = direction.normalized;
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // The projectile flies on its own each frame
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    // POLYMORPHISM
    private void OnTriggerEnter(Collider other)
    {
        // Check whether the object we hit has the base class Target
        Target enemy = other.GetComponent<Target>();
        Debug.Log("Detect a collision with " + other.name);
        if (enemy != null)
        {
            // The projectile only calls OnHit(), and the enemy reacts according to its own logic.
            enemy.OnHit();
            
            // The projectile destroys itself after the hit
            Destroy(gameObject);
        }
    }
}