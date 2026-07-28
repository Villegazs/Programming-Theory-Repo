using UnityEngine;

// 1. INHERITANCE: Base class for any weapon controlled by a player
public abstract class Weapon : MonoBehaviour
{
    // 3. ENCAPSULACIÓN: El arma controla su propia velocidad de disparo
    [SerializeField] private float fireRate = 0.5f; 
    
    private float nextFireTime = 0f;

    // Método público que el jugador llamará cuando haga clic
    public void TryShoot()
    {
        // El arma verifica si ya pasó el tiempo de recarga
        if (Time.time >= nextFireTime)
        {
            ExecuteAttack();
            nextFireTime = Time.time + fireRate; // Calculamos el próximo disparo
        }
    }

    // 4. ABSTRACCIÓN: Obligamos a las armas hijas a definir cómo atacan,
    // pero el jugador no necesita saber cómo lo hacen.
    protected abstract void ExecuteAttack();
}