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
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("WorldItem:  No se encontro el Player.  Tiene el tag Player?");
        }
        
        if (itemData == null)
        {
            Debug.LogError("WorldItem: Item Data no esta asignado en " + gameObject.name);
        }
    }
    
    void Update()
    {
        if (player == null || itemData == null) return;
        
        float distance = Vector2.Distance(transform. position, player.position);
        canPickup = distance <= pickupRange;
        
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {
            Pickup();
        }
    }
    
    void Pickup()
    {
        if (InventoryManager.Instance != null)
        {
            if (InventoryManager.Instance.AddItem(itemData, quantity))
            {
                PickupEffect effect = GetComponent<PickupEffect>();
                
                if (effect != null)
                {
                    effect. PlayPickupEffect(player);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            Debug.LogError("InventoryManager. Instance es null");
        }
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos. color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}