using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlotHandler : MonoBehaviour, IDropHandler
{
    public EquipmentManager.EquipmentSlot slotType;
    public Image iconImage;

    [Header("Manager Reference")]
    public EquipmentManager equipmentManager;

    [Header("Inventory Reference")]
    public InventoryManager inventoryManager;

    private ItemDragHandler currentEquippedIcon;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("DROP SLOT AKTYWNY!");

        ItemDragHandler dragged = eventData.pointerDrag.GetComponent<ItemDragHandler>();
        if (dragged != null && dragged.item != null)
        {
            dragged.wasDropped = true;

            if (!IsItemCompatibleWithSlot(dragged.item, slotType))
            {
                Debug.LogWarning($"❌ {dragged.item.name} nie pasuje do slotu {slotType}");

                // Return item to last parent and position
                dragged.transform.SetParent(dragged.originalParent);
                RectTransform rt = dragged.GetComponent<RectTransform>();
                rt.SetParent(inventoryManager.inventoryPanel);
                rt.anchorMin = new Vector2(0.5f, 0.5f);
                rt.anchorMax = new Vector2(0.5f, 0.5f);
                rt.pivot = new Vector2(0.5f, 0.5f);
                rt.anchoredPosition = Vector2.zero;
                dragged.wasDropped = false;
                return;
            }

            dragged.wasDropped = true;

            //Delete old item if its in the slot
            if (currentEquippedIcon != null)
            {
                Debug.Log("Przenoszę stary item z powrotem do inventory.");
                currentEquippedIcon.transform.SetParent(inventoryManager.inventoryPanel);
                currentEquippedIcon.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
            else
            {
                Debug.Log("Brak starego itemu do przeniesienia.");
            }

            //Remember new item
            currentEquippedIcon = dragged;

            //  assign to manager
            if (equipmentManager != null)
            {
                equipmentManager.EquipItem(dragged.item, slotType);
            }


            //  hide slot background
            if (iconImage != null)
            {
                iconImage.sprite = null;
                iconImage.color = Color.clear;
            }

            //  Place item icon in slot
            dragged.transform.SetParent(this.transform);
            dragged.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            Debug.Log($"✅ Equipped {dragged.item.name} in {slotType} slot");

        }
    }

    // Funkcja do sprawdzania kompatybilności
    private bool IsItemCompatibleWithSlot(Item item, EquipmentManager.EquipmentSlot slot)
    {
        return (item.itemType.ToString() == slot.ToString());
    }

    //public void OnDrop(PointerEventData eventData)
    //{
    //    Debug.Log("DROP SLOT AKTYWNY!"); // 

    //    ItemDragHandler dragged = eventData.pointerDrag.GetComponent<ItemDragHandler>();
    //    if (dragged != null && dragged.item != null)
    //    {
    //        if (equipmentManager != null)
    //        {
    //            equipmentManager.EquipItem(dragged.item, slotType);

    //            // Aktualizacja UI
    //            if (iconImage != null && dragged.item.icon != null)
    //            {
    //                iconImage.sprite = dragged.item.icon;
    //                iconImage.color = Color.white;
    //            }

    //            Debug.Log($"Equipped {dragged.item.name} in {slotType} slot");
    //        }
    //        else
    //        {
    //            Debug.LogWarning("Brakuje EquipmentManager w EquipmentSlotHandler!");
    //        }
    //    }
    //}
}
