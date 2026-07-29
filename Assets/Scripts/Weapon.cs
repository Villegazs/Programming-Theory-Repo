using UnityEngine;

// INHERITANCE
public abstract class Weapon : MonoBehaviour
{
    // ENCAPSULATION
    [SerializeField] private float fireRate = 0.5f; 
    
    private float nextFireTime = 0f;

    // Public method that the player will call when clicking
    public void TryShoot()
    {
        // The weapon checks whether the cooldown has already passed
        if (Time.time >= nextFireTime)
        {
            ExecuteAttack();
            nextFireTime = Time.time + fireRate; // Calculate the next shot
        }
    }

    // ABSTRACTION
    protected abstract void ExecuteAttack();
}