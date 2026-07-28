using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // 3. ENCAPSULACIÓN: Propiedades de vuelo de la bala
    [SerializeField] private float speed = 15f;
    [SerializeField] private float lifetime = 3f; // Se destruye si no golpea nada
    
    private Vector3 moveDirection;

    // Método público para que el arma le diga hacia dónde volar al momento de nacer
    public void Initialize(Vector3 direction)
    {
        moveDirection = direction.normalized;
        
        // ABSTRACCIÓN: Limpieza automática. Le decimos a Unity que destruya 
        // este objeto después de 'lifetime' segundos si se pierde en el vacío.
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // La bala vuela de forma autónoma en cada frame
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    // 2. POLIMORFISMO EN ACCIÓN: Detección de colisiones
    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si lo que golpeamos tiene la clase base Target
        Target enemy = other.GetComponent<Target>();
        Debug.Log("Detect a collision with " + other.name);
        if (enemy != null)
        {
            // ¡Aquí ocurre la magia! No importa si es el Cubo Normal o el Cilindro Blindado.
            // La bala solo llama a OnHit() y el enemigo reacciona según su propia programación.
            enemy.OnHit();
            
            // La bala se destruye a sí misma tras el impacto
            Destroy(gameObject);
        }
    }
}