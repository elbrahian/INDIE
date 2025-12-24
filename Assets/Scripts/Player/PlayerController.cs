using UnityEngine;

public class PlayerController :  MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 1.5f;
    
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    private Vector2 moveInput;
    private bool isSprinting;
    
    void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (animator == null) animator = GetComponent<Animator>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        // Input
        moveInput. x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
        
        isSprinting = Input.GetKey(KeyCode.LeftShift);
        
        // Animacion ( cuando tenga sprites)
        UpdateAnimation();
    }
    
    void FixedUpdate()
    {
        // Movimiento
        float currentSpeed = isSprinting ? moveSpeed * sprintMultiplier :  moveSpeed;
        rb.linearVelocity = moveInput * currentSpeed;
    }
    
    void UpdateAnimation()
    {
        // Por ahora solo voltear sprite 
        if (moveInput. x != 0)
        {
            spriteRenderer.flipX = moveInput.x < 0;
        }
        
        // Cuando tenga animaciones, descomentar: 
        // animator.SetFloat("Speed", moveInput.magnitude);
        // animator.SetFloat("Horizontal", moveInput.x);
        // animator.SetFloat("Vertical", moveInput.y);
    }
}