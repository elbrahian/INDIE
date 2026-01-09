using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentSlot : MonoBehaviour
{
    [Header("Slot Info")]
    [SerializeField] private EquipmentType slotType;
    
    [Header("References")]
    [SerializeField] private Image slotIcon;
    [SerializeField] private TextMeshProUGUI slotTypeText;
    [SerializeField] private TextMeshProUGUI equippedItemText;
    
    [Header("Visual")]
    [SerializeField] private Color emptyColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    [SerializeField] private Color filledColor = Color.white;
    
    private Item equippedItem;
    
    void Awake()
    {
        // Auto-asignar referencias
        if (slotIcon == null)
            slotIcon = transform.Find("SlotIcon")?.GetComponent<Image>();
        
        if (slotTypeText == null)
            slotTypeText = transform.Find("SlotTypeText")?.GetComponent<TextMeshProUGUI>();
        
        if (equippedItemText == null)
            equippedItemText = transform.Find("EquippedItemText")?.GetComponent<TextMeshProUGUI>();
    }
    
    void Start()
    {
        UpdateSlotDisplay();
    }
    
    public void EquipItem(Item item)
    {
        if (item == null || ! item.isEquippable || item.equipmentType != slotType)
        {
            Debug.LogWarning($"⚠️ No se puede equipar {item?. itemName} en slot {slotType}");
            return;
        }
        
        equippedItem = item;
        UpdateSlotDisplay();
        
        Debug.Log($"✅ Equipado:  {item.itemName} en {slotType}");
    }
    
    public void UnequipItem()
    {
        if (equippedItem != null)
        {
            Debug.Log($"❌ Desequipado: {equippedItem.itemName}");
            equippedItem = null;
            UpdateSlotDisplay();
        }
    }
    
    void UpdateSlotDisplay()
    {
        if (equippedItem != null)
        {
            // Slot con item equipado
            if (slotIcon != null)
            {
                slotIcon. sprite = equippedItem.icon;
                slotIcon.color = filledColor;
            }
            
            if (equippedItemText != null)
            {
                equippedItemText.text = equippedItem.itemName;
                equippedItemText.color = Color.white;
            }
        }
        else
        {
            // Slot vacío
            if (slotIcon != null)
            {
                slotIcon.sprite = null;
                slotIcon.color = emptyColor;
            }
            
            if (equippedItemText != null)
            {
                equippedItemText.text = "(Vacío)";
                equippedItemText.color = new Color(0.7f, 0.7f, 0.7f);
            }
        }
    }
    
    public Item GetEquippedItem() => equippedItem;
    public EquipmentType GetSlotType() => slotType;
    public bool IsEmpty() => equippedItem == null;
}