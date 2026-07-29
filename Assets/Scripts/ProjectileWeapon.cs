using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    // POLYMORPHISM
    protected override void ExecuteAttack()
    {
        // Create the projectile
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        
        Projectile projectileScript = bullet.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            // Tell the projectile to travel in the direction the weapon barrel is pointing
            projectileScript.Initialize(firePoint.forward);
        }
    }
}