using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI quantityText;
    
    [Header("Visual Settings")]
    [SerializeField] private Color emptyColor = new Color(0.15f, 0.15f, 0.15f, 1f);
    [SerializeField] private Color filledColor = new Color(0.25f, 0.25f, 0.25f, 1f);
    
    private Item currentItem;
    private int currentQuantity;
    private Image slotBackground;
    
    void Awake()
    {
        slotBackground = GetComponent<Image>();
        
        // Auto-asignar referencias
        if (itemIcon == null)
        {
            Transform iconTransform = transform.Find("ItemIcon");
            if (iconTransform != null)
                itemIcon = iconTransform. GetComponent<Image>();
        }
        
        if (quantityText == null)
        {
            Transform textTransform = transform.Find("QuantityText");
            if (textTransform != null)
                quantityText = textTransform.GetComponent<TextMeshProUGUI>();
        }
    }
    
    public void SetItem(Item item, int quantity)
    {
        currentItem = item;
        currentQuantity = quantity;
        
        if (item != null && quantity > 0)
        {
            // Verificar referencias
            if (itemIcon == null || quantityText == null)
            {
                Debug.LogError($"❌ Referencias faltantes en {gameObject.name}");
                return;
            }
            
            // Asignar icono
            if (item.icon != null)
            {
                itemIcon.sprite = item.icon;
                itemIcon.enabled = true;
                itemIcon.color = Color.white;
            }
            else
            {
                Debug.LogWarning($"⚠️ Item '{item.itemName}' no tiene icono");
                itemIcon.enabled = false;
            }
            
            // ⭐ SIEMPRE MOSTRAR LA CANTIDAD
            quantityText. text = quantity.ToString();
            quantityText.gameObject.SetActive(quantity > 1); // ← CAMBIO CLAVE
            
            // Color de fondo
            if (slotBackground != null)
                slotBackground.color = filledColor;
                
            Debug.Log($"✅ Slot configurado: {item.itemName} x{quantity}");
        }
        else
        {
            ClearSlot();
        }
    }
    
    public void ClearSlot()
    {
        currentItem = null;
        currentQuantity = 0;
        
        if (itemIcon != null)
        {
            itemIcon.sprite = null;
            itemIcon.enabled = false;
        }
        
        if (quantityText != null)
        {
            quantityText. text = "";
            quantityText.gameObject.SetActive(false); // ← Ocultar cuando vacío
        }
        
        if (slotBackground != null)
            slotBackground.color = emptyColor;
    }
    
    public Item GetItem() => currentItem;
    public int GetQuantity() => currentQuantity;
    public bool IsEmpty() => currentItem == null;
}