using UnityEngine;

public class WorldItem : MonoBehaviour
{
    [SerializeField] private Item itemData;
    [SerializeField] private int quantity = 1;
    [SerializeField] private float pickupRange = 1.5f;
    
    private Transform player;
    private bool canPickup = false;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }
    
    void Update()
    {
        if (player == null) return;


        float distance = Vector2.Distance(transform.position, player.position);
        canPickup = distance <= pickupRange;
        
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {
            Pickup();
        }
    }
    
    void Pickup()
    {
        if (InventoryManager.Instance. AddItem(itemData, quantity))
        {
            Destroy(gameObject);
        }
    }
    
    // Visualizar rango en el editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color. yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}