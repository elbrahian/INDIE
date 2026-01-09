// Assets/Scripts/Effects/FloatingItem.cs

using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    [Header("Float Settings")]
    [SerializeField] private float floatAmplitude = 0.2f; // Altura del movimiento
    [SerializeField] private float floatSpeed = 2f; // Velocidad
    
    [Header("Rotation")]
    [SerializeField] private bool rotateItem = true;
    [SerializeField] private float rotationSpeed = 50f; // Grados por segundo
    
    [Header("Scale Pulse")]
    [SerializeField] private bool pulseScale = false;
    [SerializeField] private float pulseAmount = 0.1f;
    [SerializeField] private float pulseSpeed = 3f;
    
    private Vector3 startPosition;
    private Vector3 startScale;
    private float randomOffset;
    
    void Start()
    {
        startPosition = transform.position;
        startScale = transform.localScale;
        
        // Offset aleatorio para que no todos floten sincronizados
        randomOffset = Random.Range(0f, 2f * Mathf.PI);
    }
    
    void Update()
    {
        // Flotación (movimiento arriba/abajo)
        float newY = startPosition.y + Mathf. Sin((Time.time * floatSpeed) + randomOffset) * floatAmplitude;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
        
        // Rotación
        if (rotateItem)
        {
            transform. Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        
        // Pulso de escala
        if (pulseScale)
        {
            float scale = 1f + Mathf.Sin((Time.time * pulseSpeed) + randomOffset) * pulseAmount;
            transform.localScale = startScale * scale;
        }
    }
}