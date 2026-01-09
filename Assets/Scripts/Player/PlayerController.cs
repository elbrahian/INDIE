using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 1.5f;
    
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    private Vector2 moveInput;
    private Vector2 lastMoveDirection = Vector2.down;
    
    // Hash de parámetros
    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int HorizontalHash = Animator.StringToHash("Horizontal");
    private static readonly int VerticalHash = Animator.StringToHash("Vertical");
    
    void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (animator == null) animator = GetComponentInChildren<Animator>();
        if (spriteRenderer == null) spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    
    void Start()
    {
        if (animator != null)
        {
            // Inicializar:  quieto mirando abajo
            animator.SetFloat(HorizontalHash, 0f);
            animator.SetFloat(VerticalHash, -1f);
            animator.SetFloat(SpeedHash, 0f);
            
        }
    }
    
    void Update()
    {
        // Input
        moveInput. x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
        
        UpdateAnimation();
    }
    
    void FixedUpdate()
    {
        // Movimiento
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isSprinting ?  moveSpeed * sprintMultiplier :  moveSpeed;
        rb.linearVelocity = moveInput * currentSpeed;
    }
    
    void UpdateAnimation()
    {
        if (animator == null) return;
        
        float speed = moveInput.magnitude;
        
        // ⭐ ACTUALIZAR SPEED (esto controla Idle ⇄ Walk)
        animator.SetFloat(SpeedHash, speed);
        
        if (speed > 0.01f)
        {
            // Está moviéndose → guardar dirección
            lastMoveDirection = moveInput;
            
            // Actualizar dirección actual
            animator.SetFloat(HorizontalHash, moveInput.x);
            animator.SetFloat(VerticalHash, moveInput.y);
            
        }
        else
        {
            // Está quieto → mantener última dirección para idle direccional
            animator.SetFloat(HorizontalHash, lastMoveDirection.x);
            animator. SetFloat(VerticalHash, lastMoveDirection.y);
            
        }
    }
}