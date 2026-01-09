using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }
    
    [Header("References")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Transform slotsContainer;
    [SerializeField] private GameObject slotPrefab;
    
    [Header("Settings")]
    [SerializeField] private int numberOfSlots = 20;
    [SerializeField] private KeyCode toggleKey = KeyCode.I;
    
    [Header("Equipment References")]
    [SerializeField] private EquipmentSlot helmetSlot;
    [SerializeField] private EquipmentSlot chestSlot;
    [SerializeField] private EquipmentSlot bootsSlot;
    [SerializeField] private EquipmentSlot weaponSlot;

    private List<InventorySlot> slots = new List<InventorySlot>();
    private bool isOpen = false;
    
    void Awake()
    {
        Debug.Log("🔧 InventoryUI Awake llamado");
        
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("⚠️ Instancia duplicada de InventoryUI, destruyendo.. .");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        // Verificar referencias
        if (inventoryPanel == null)
            Debug.LogError("❌ Inventory Panel no está asignado en InventoryUI");
        if (slotsContainer == null)
            Debug.LogError("❌ Slots Container no está asignado en InventoryUI");
        if (slotPrefab == null)
            Debug.LogError("❌ Slot Prefab no está asignado en InventoryUI");
    }
    
    void Start()
    {
        Debug.Log("🔧 InventoryUI Start llamado");
        CreateSlots();
        CloseInventory();
    }
    
    void Update()
    {
        // Debug:  detectar CUALQUIER presión de I
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("🔑 Tecla I presionada (hardcoded)");
        }
        
        if (Input.GetKeyDown(toggleKey))
        {
            Debug.Log($"🔑 ToggleKey ({toggleKey}) detectada");
            ToggleInventory();
        }
    }
    
    void CreateSlots()
    {
        if (slotsContainer == null)
        {
            Debug.LogError("❌ No se puede crear slots:  slotsContainer es null");
            return;
        }
        
        if (slotPrefab == null)
        {
            Debug.LogError("❌ No se puede crear slots: slotPrefab es null");
            return;
        }
        
        // Limpiar slots existentes
        foreach (Transform child in slotsContainer)
        {
            Destroy(child.gameObject);
        }
        slots.Clear();
        
        // Crear nuevos slots
        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotsContainer);
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            
            if (slot != null)
            {
                slots.Add(slot);
                slot.ClearSlot();
            }
            else
            {
                Debug. LogError($"❌ El prefab {slotPrefab. name} no tiene InventorySlot");
            }
        }
        
        Debug.Log($"✅ Creados {slots.Count} slots de inventario");
    }
    
    public void UpdateUI()
    {
        if (InventoryManager.Instance == null)
        {
            Debug.LogWarning("⚠️ InventoryManager no existe");
            return;
        }
        
        var items = InventoryManager.Instance. GetAllItems();
        
        // Limpiar slots
        foreach (var slot in slots)
        {
            slot. ClearSlot();
        }
        
        // Llenar slots
        int slotIndex = 0;
        foreach (var kvp in items)
        {
            if (slotIndex >= slots.Count) break;
            
            slots[slotIndex].SetItem(kvp.Key, kvp. Value);
            slotIndex++;
        }
        
        Debug.Log($"🔄 UI actualizada:  {slotIndex} items mostrados de {items.Count} totales");
    }
    
    public void ToggleInventory()
    {
        Debug.Log($"📂 ToggleInventory llamado.  Estado actual: {(isOpen ? "ABIERTO" : "CERRADO")}");
        
        if (isOpen)
            CloseInventory();
        else
            OpenInventory();
    }
    
    public void OpenInventory()
    {
        if (inventoryPanel == null)
        {
            Debug.LogError("❌ No se puede abrir:  inventoryPanel es null");
            return;
        }
        
        isOpen = true;
        inventoryPanel.SetActive(true);
        UpdateUI();
        UpdateEquipmentUI();
        Time.timeScale = 0f;
        
        Debug.Log("✅ Inventario ABIERTO");
    }
    
    public void CloseInventory()
    {
        if (inventoryPanel == null)
        {
            Debug.LogError("❌ No se puede cerrar: inventoryPanel es null");
            return;
        }
        
        isOpen = false;
        inventoryPanel.SetActive(false);
        Time.timeScale = 1f;
        
        Debug.Log("✅ Inventario CERRADO");
    }
    
    public bool IsOpen() => isOpen;
    
    // Debug: llamar manualmente desde Console
    [ContextMenu("Abrir Inventario")]
    void DebugOpen()
    {
        OpenInventory();
    }
    
    [ContextMenu("Cerrar Inventario")]
    void DebugClose()
    {
        CloseInventory();
    }

    public void UpdateEquipmentUI()
    {
        if (EquipmentManager.Instance == null) return;
        
        var equipped = EquipmentManager.Instance.GetAllEquippedItems();
        
        // Actualizar cada slot visual
        UpdateEquipmentSlotUI(helmetSlot, EquipmentType.Helmet, equipped);
        UpdateEquipmentSlotUI(chestSlot, EquipmentType.Chest, equipped);
        UpdateEquipmentSlotUI(bootsSlot, EquipmentType. Boots, equipped);
        UpdateEquipmentSlotUI(weaponSlot, EquipmentType.Weapon, equipped);
    }

    void UpdateEquipmentSlotUI(EquipmentSlot slot, EquipmentType type, Dictionary<EquipmentType, Item> equipped)
    {
        if (slot == null) return;
        
        if (equipped.ContainsKey(type))
        {
            slot. EquipItem(equipped[type]);
        }
        else
        {
            slot.UnequipItem();
        }
    }
}