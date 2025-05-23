using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlotHandler : MonoBehaviour, IDropHandler
{
    public EquipmentManager.EquipmentSlot slotType;
    public Image iconImage;

    [Header("Manager Reference")]
    public EquipmentManager equipmentManager;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("DROP SLOT AKTYWNY!"); // 

        ItemDragHandler dragged = eventData.pointerDrag.GetComponent<ItemDragHandler>();
        if (dragged != null && dragged.item != null)
        {
            if (equipmentManager != null)
            {
                equipmentManager.EquipItem(dragged.item, slotType);

                // Aktualizacja UI
                if (iconImage != null && dragged.item.icon != null)
                {
                    iconImage.sprite = dragged.item.icon;
                    iconImage.color = Color.white;
                }

                Debug.Log($"Equipped {dragged.item.name} in {slotType} slot");
            }
            else
            {
                Debug.LogWarning("Brakuje EquipmentManager w EquipmentSlotHandler!");
            }
        }
    }
}
