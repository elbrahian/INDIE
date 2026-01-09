using UnityEngine;

// Esta línea permite crear items desde el menú de Unity
[CreateAssetMenu(fileName = "NewItem", menuName = "Game/Item")]
public class Item : ScriptableObject
{
    // ===== INFORMACIÓN BÁSICA =====
    [Header("Basic Info")]
    public string itemName = "Nuevo Item";
    public Sprite icon; // La imagen del item
    public ItemType type; // Qué tipo de item es
    
    // ===== STACK Y CANTIDAD =====
    [Header("Stacking")]
    [Tooltip("Cantidad máxima que puede apilarse")]
    public int maxStackSize = 99;
    
    // ===== DESCRIPCIÓN =====
    [Header("Description")]
    [TextArea(3, 5)] // Caja de texto más grande
    public string description = "Descripción del item";
    
    // ===== VALORES (Opcional) =====
    [Header("Values")]
    public int sellPrice = 10;
    public int buyPrice = 20;
    
    // ===== PROPIEDADES DE USO (Opcional) =====
    [Header("Usage")]
    public bool isConsumable = false; // ¿Se consume al usar?
    public float useTime = 1f; // Tiempo para usar (segundos)
    
    // ===== SI ES COMIDA =====
    [Header("Food Properties (If applicable)")]
    public int healthRestore = 0;
    public int hungerRestore = 0;
    
    // ===== SI ES HERRAMIENTA/ARMA =====
    [Header("Tool/Weapon Properties (If applicable)")]
    public int damage = 0;
    public float attackSpeed = 1f;
    public int durability = 100; // Durabilidad máxima

    [Header("Equipment Properties")]
    public bool isEquippable = false;
    public EquipmentType equipmentType;
    public int defense = 0; // Para armaduras
    public float moveSpeedBonus = 0f; // Bonus de velocidad
}

// Enumeración de tipos de items
public enum ItemType
{
    Resource,        // Recurso básico (madera, piedra)
    Tool,           // Herramienta (pico, hacha)
    Weapon,         // Arma (espada, arco)
    Food,           // Comida consumible
    Seed,           // Semilla para plantar
    BuildingMaterial, // Material de construcción procesado
    Consumable,     // Consumible general (poción)
    QuestItem       // Item de misión
}