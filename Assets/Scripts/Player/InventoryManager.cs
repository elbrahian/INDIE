using System.Collections. Generic;
using UnityEngine;

public class InventoryManager :  MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    
    [SerializeField] private int inventorySize = 20;
    private Dictionary<Item, int> items = new Dictionary<Item, int>();
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Update()
    {
    // ⭐ SOLO imprimir en consola si el UI NO está abierto
        if (Input.GetKeyDown(KeyCode.I))
        {
        // Si existe el UI y está manejando la tecla, no hacer nada aquí
        if (InventoryUI.Instance != null)
        {
            // El UI ya maneja la tecla, no hacer nada
            return;
        }
        
        // Fallback: si no hay UI, imprimir en consola
        PrintInventory();
        }
    }
    
    public bool AddItem(Item item, int quantity)
    {
        if (item == null)
        {
            Debug.LogError("❌ Item es null");
            return false;
        }
        
        if (items.ContainsKey(item))
        {
            items[item] += quantity;
        }
        else
        {
            if (items.Count >= inventorySize)
            {
                Debug.LogWarning("⚠️ Inventario lleno");
                return false;
            }
            items. Add(item, quantity);
        }
        
        Debug.Log($"✅ {item.itemName} x{quantity} agregado.  Total: {items[item]}");
        
        // ⭐ Actualizar UI si está abierto
        if (InventoryUI.Instance != null && InventoryUI.Instance.IsOpen())
        {
            InventoryUI.Instance.UpdateUI();
        }
        
        return true;
    }
    
    public bool RemoveItem(Item item, int quantity)
    {
        if (! items.ContainsKey(item) || items[item] < quantity)
        {
            Debug.LogWarning($"❌ No hay suficiente {item.itemName}");
            return false;
        }
        
        items[item] -= quantity;
        if (items[item] <= 0)
        {
            items. Remove(item);
        }
        
        // Actualizar UI
        if (InventoryUI.Instance != null && InventoryUI.Instance.IsOpen())
        {
            InventoryUI.Instance.UpdateUI();
        }
        
        return true;
    }
    
    public int GetItemCount(Item item)
    {
        return items. ContainsKey(item) ? items[item] : 0;
    }
    
    // ⭐ NUEVO: Método para que el UI obtenga todos los items
    public Dictionary<Item, int> GetAllItems()
    {
        return new Dictionary<Item, int>(items);
    }
    
    public void PrintInventory()
    {
        Debug.Log("====== 🎒 INVENTARIO ======");
        
        if (items.Count == 0)
        {
            Debug.Log("(Vacío)");
            return;
        }
        
        foreach (var kvp in items)
        {
            Debug.Log($"📦 {kvp.Key.itemName}:  x{kvp.Value}");
        }
        
        Debug.Log($"Slots usados: {items.Count}/{inventorySize}");
        Debug.Log("===========================");
    }
}