using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public Playerscript player;

    public enum EquipmentSlot
    {
        Helmet,
        Armor,
        Pants,
        Boots,
        WeaponMain,
        WeaponOff,
        Amulet
    }

    [System.Serializable]
    public class EquippedItem
    {
        public EquipmentSlot slot;
        public Item item;
    }

    public List<EquippedItem> equippedItems = new List<EquippedItem>();

    public void EquipItem(Item item, EquipmentSlot slot)
    {
        // Removing old item from the slot
        EquippedItem existing = equippedItems.Find(e => e.slot == slot);
        if (existing != null)
        {
            RemoveItemEffects(existing.item);
            equippedItems.Remove(existing);
        }

        // Add new
        equippedItems.Add(new EquippedItem { item = item, slot = slot });
        ApplyItemEffects(item);
    }

    public void RemoveItem(Item item)
    {
        EquippedItem equipped = equippedItems.Find(e => e.item == item);
        if (equipped != null)
        {
            RemoveItemEffects(equipped.item);
            equippedItems.Remove(equipped);
        }
    }

    void ApplyItemEffects(Item item)
    {
        if (player != null)
        {
            player.damage += item.damage;
            // armnor etc.
        }
    }

    void RemoveItemEffects(Item item)
    {
        if (player != null)
        {
            player.damage -= item.damage;
            // etc.
        }
    }
}
