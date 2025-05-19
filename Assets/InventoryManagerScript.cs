using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public ItemDatabase itemDatabase;
    public Playerscript player;

    private List<Item> unlockedItems = new List<Item>();
    void Start()
    {
        LoadUnlockedItems();
        ApplyItemEffectsToPlayer();
    }

    public void LoadUnlockedItems()
    {
        unlockedItems.Clear();

        foreach (int id in ProgressionManager.Instance.data.unlockedItemsIds)
        {
            Item item = itemDatabase.GetItemByID(id);
            if (item != null)
            {
                unlockedItems.Add(item);
                Debug.Log("Unlocked item: " + item.name);
            }
        }


        Debug.Log("Za³adowano " + unlockedItems.Count + " odblokowanych przedmiotów.");
    }
    void ApplyItemEffectsToPlayer()
    {
        if (player == null)
        {
            Debug.LogWarning("Missing player reference in InventoryManager!");
            return;
        }

        foreach (Item item in unlockedItems)
        {
            player.damage += item.damage;
            Debug.Log("Zastosowano efekt przedmiotu: " + item.name);
        }
    }
}
