using UnityEngine;

// Required that the object has a Rigidbody for physics to work
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    // ENCAPSULATION
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
        
        // Prevent the player from falling or rotating strangely when colliding with something
        rb.freezeRotation = true; 
        
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // ABSTRACTION
        ProcessInputs();
        CalculateMouseAim();
        HandleShooting();
    }

    private void FixedUpdate()
    {
        // ABSTRACTION
        MovePlayer();
        RotatePlayer();
    }

    private void ProcessInputs()
    {
        // Capture WASD keys or arrow keys
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        // .normalized ensures diagonal movement does not make the character move faster
        movementInput = new Vector3(moveX, 0f, moveZ).normalized;
    }

    private void MovePlayer()
    {
        // Calculate the new position and tell the physics engine to move us there
        Vector3 newPosition = rb.position + movementInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }
    private void CalculateMouseAim()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        
        if (groundPlane.Raycast(cameraRay, out float rayLength))
        {
            // Only store the coordinates
            pointToLook = cameraRay.GetPoint(rayLength);
        }
    }
    
    private void RotatePlayer()
    {
        // We use rb.position
        Vector3 directionToLook = pointToLook - rb.position;
        directionToLook.y = 0f;

        // sqrMagnitude is faster to compute than Vector3.Distance
        // This prevents jittering when the cursor is exactly over the player
        if (directionToLook.sqrMagnitude > 0.05f) 
        {
            // Create the correct rotation
            Quaternion targetRotation = Quaternion.LookRotation(directionToLook);
            
            // Tell the physics engine to rotate the object safely
            rb.MoveRotation(targetRotation); 
        }
    }
    
    // --- NEW: SHOOTING SYSTEM ---
    private void HandleShooting()
    {
        // If the player left-clicks and has an equipped weapon, they can shoot
        if (Input.GetMouseButton(0) && equippedWeapon != null)
        {
            // ABSTRACTION
            equippedWeapon.TryShoot();
        }
    }

    // ENCAPSULATION
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Player hit! Remaining health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // ABSTRACTION
        Debug.Log("Game Over. End of the match.");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.EndGame();
        }
        
        
        gameObject.SetActive(false); // Make the player disappear
    }
}