// Assets/Scripts/Effects/PickupEffect.cs

using UnityEngine;
using System.Collections;

public class PickupEffect : MonoBehaviour
{
    [Header("Pickup Effect")]
    [SerializeField] private GameObject pickupParticlePrefab;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private float moveToPlayerSpeed = 10f; // Ahora SÍ se usa
    [SerializeField] private bool animateToPlayer = true;
    
    private bool isBeingPickedUp = false;
    private Transform playerTransform;
    
    public void PlayPickupEffect(Transform player)
    {
        if (isBeingPickedUp) return;
        
        isBeingPickedUp = true; 
        playerTransform = player;
        
        // Reproducir sonido
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }
        
        // Spawn partículas
        if (pickupParticlePrefab != null)
        {
            Instantiate(pickupParticlePrefab, transform. position, Quaternion.identity);
        }
        
        if (animateToPlayer)
        {
            StartCoroutine(MoveToPlayerCoroutine());
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private IEnumerator MoveToPlayerCoroutine()
    {
        float elapsed = 0f;
        // AQUÍ SE USA moveToPlayerSpeed
        float duration = Vector3.Distance(transform.position, playerTransform.position) / moveToPlayerSpeed;
        duration = Mathf.Clamp(duration, 0.2f, 0.5f); // Entre 0.2 y 0.5 segundos
        
        Vector3 startPos = transform.position;
        
        // Desactivar colisión
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;
        
        while (elapsed < duration && playerTransform != null)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            
            // Ease out cubic para movimiento suave
            t = 1f - Mathf. Pow(1f - t, 3f);
            
            transform.position = Vector3.Lerp(startPos, playerTransform.position, t);
            
            // Reducir escala
            transform.localScale = Vector3.one * (1f - t);
            
            yield return null;
        }
        
        Destroy(gameObject);
    }
}