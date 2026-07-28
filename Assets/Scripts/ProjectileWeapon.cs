using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    // 2. POLIMORFISMO: Implementamos el disparo directo
    protected override void ExecuteAttack()
    {
        // Creamos la bala
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        
        Projectile projectileScript = bullet.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            // Le decimos a la bala que viaje hacia donde apunta el cañón del arma
            projectileScript.Initialize(firePoint.forward);
        }
    }
}