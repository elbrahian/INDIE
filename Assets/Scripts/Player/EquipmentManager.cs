using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance { get; private set; }
    
    private Dictionary<EquipmentType, Item> equippedItems = new Dictionary<EquipmentType, Item>();
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    public bool EquipItem(Item item)
    {
        if (item == null || !item.isEquippable)
        {
            Debug.LogWarning("⚠️ Item no es equipable");
            return false;
        }
        
        EquipmentType slot = item.equipmentType;
        
        // Si ya hay algo equipado, desequipar primero
        if (equippedItems.ContainsKey(slot))
        {
            UnequipItem(slot);
        }
        
        // Equipar nuevo item
        equippedItems[slot] = item;
        
        // Aplicar stats (velocidad, defensa, etc.)
        ApplyItemStats(item, true);
        
        Debug.Log($"✅ Equipado: {item.itemName}");
        return true;
    }
    
    public bool UnequipItem(EquipmentType slot)
    {
        if (! equippedItems.ContainsKey(slot))
        {
            return false;
        }
        
        Item item = equippedItems[slot];
        
        // Remover stats
        ApplyItemStats(item, false);
        
        // Devolver al inventario
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.AddItem(item, 1);
        }
        
        equippedItems.Remove(slot);
        
        Debug.Log($"❌ Desequipado: {item.itemName}");
        return true;
    }
    
    void ApplyItemStats(Item item, bool apply)
    {
        float multiplier = apply ? 1f : -1f;
        
        // Aplicar bonus de velocidad
        if (item.moveSpeedBonus != 0)
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            if (player != null)
            {
                // Aquí modificarías la velocidad del player
                // player.AddSpeedBonus(item.moveSpeedBonus * multiplier);
                Debug.Log($"🏃 Velocidad {(apply ? "aumentada" : "reducida")} por {item.moveSpeedBonus}");
            }
        }
        
        // Aplicar defensa, etc.
        if (item.defense != 0)
        {
            Debug.Log($"🛡️ Defensa {(apply ?  "aumentada" : "reducida")} por {item.defense}");
        }
    }
    
    public Item GetEquippedItem(EquipmentType slot)
    {
        return equippedItems.ContainsKey(slot) ? equippedItems[slot] : null;
    }
    
    public Dictionary<EquipmentType, Item> GetAllEquippedItems()
    {
        return new Dictionary<EquipmentType, Item>(equippedItems);
    }
}