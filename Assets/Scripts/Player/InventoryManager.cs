using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
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
        Debug.Log(" InventoryManager inicializado correctamente");
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log(" Tecla I presionada");
            PrintInventory();
        }
    }
    
    public bool AddItem(Item item, int quantity)
    {
        if (item == null)
        {
            Debug.LogError(" Intentando agregar un item null");
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
                Debug.Log(" Inventario lleno!");
                return false;
            }
            items.Add(item, quantity);
        }
        
        Debug.Log($" Agregado: {item. itemName} x{quantity}.  Total: {items[item]}");
        return true;
    }
    
    public bool RemoveItem(Item item, int quantity)
    {
        if (! items.ContainsKey(item) || items[item] < quantity)
        {
            Debug. Log(" No tienes suficientes items");
            return false;
        }
        
        items[item] -= quantity;
        if (items[item] <= 0)
        {
            items.Remove(item);
        }
        
        return true;
    }
    
    public int GetItemCount(Item item)
    {
        return items.ContainsKey(item) ? items[item] : 0;
    }
    
    public void PrintInventory()
    {
        Debug.Log("====== INVENTARIO ======");
        
        if (items.Count == 0)
        {
            Debug.Log("(Inventario vacío)");
            return;
        }
        
        foreach (var kvp in items)
        {
            Debug.Log($"📦 {kvp.Key.itemName}: x{kvp.Value}");
        }
        
        Debug.Log("===========================");
    }
}