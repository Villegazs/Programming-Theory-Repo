using UnityEngine;

// Requerimos que el objeto tenga un Rigidbody para que las físicas funcionen
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    // 3. ENCAPSULACIÓN: Protegemos la velocidad y la vida.
    // Solo se pueden ajustar desde el Inspector de Unity, no desde otros scripts.
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int maxHealth = 3;

    [SerializeField] private Weapon equippedWeapon;
    
    private int currentHealth;
    private Rigidbody rb;
    private Vector3 movementInput;
    private Camera mainCamera;
    private Vector3 pointToLook;

    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        
        // Evitamos que el jugador se caiga o rote de forma extraña si choca con algo
        rb.freezeRotation = true; 
        
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // 4. ABSTRACCIÓN: Mantenemos el Update limpio. Solo le decimos "lee los controles".
        ProcessInputs();
        CalculateMouseAim();
        HandleShooting();
    }

    private void FixedUpdate()
    {
        // ABSTRACCIÓN: Aplicamos el movimiento en FixedUpdate, que es el ciclo 
        // correcto para manejar físicas en Unity sin que haya tirones.
        MovePlayer();
        RotatePlayer();
    }

    private void ProcessInputs()
    {
        // Captura las teclas WASD o las flechas direccionales
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        // .normalized asegura que moverse en diagonal no te haga ir más rápido
        movementInput = new Vector3(moveX, 0f, moveZ).normalized;
    }

    private void MovePlayer()
    {
        // Calculamos la nueva posición y le decimos al motor de físicas que nos mueva allí
        Vector3 newPosition = rb.position + movementInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }
    private void CalculateMouseAim()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        
        if (groundPlane.Raycast(cameraRay, out float rayLength))
        {
            // Solo guardamos las coordenadas
            pointToLook = cameraRay.GetPoint(rayLength);
        }
    }
    
    private void RotatePlayer()
    {
        // We use rb.position
        Vector3 directionToLook = pointToLook - rb.position;
        directionToLook.y = 0f;

        // sqrMagnitude es más rápido de calcular que Vector3.Distance
        // Esto evita el jittering si el cursor está exactamente sobre el jugador
        if (directionToLook.sqrMagnitude > 0.05f) 
        {
            // Creamos la rotación correcta
            Quaternion targetRotation = Quaternion.LookRotation(directionToLook);
            
            // Le decimos al motor de físicas que rote el objeto de forma segura
            rb.MoveRotation(targetRotation); 
        }
    }
    
    // --- NUEVO: SISTEMA DE DISPARO ---
    private void HandleShooting()
    {
        // If the player left click & has an equipped weapon can shoot
        if (Input.GetMouseButton(0) && equippedWeapon != null)
        {
            // ABSTRACTION: The player try to shoot, the weapon has the final verdict
            equippedWeapon.TryShoot();
        }
    }

    // 3. ENCAPSULATION 
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"¡Jugador herido! Vida restante: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // ABSTRACTION
        Debug.Log("Game Over. Fin de la partida.");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.EndGame();
        }
        
        
        gameObject.SetActive(false); // Dissapear the player
    }
}