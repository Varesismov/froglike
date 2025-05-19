using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public ItemDatabase itemDatabase;

    private List<Item> unlockedItems = new List<Item>();

    void Start()
    {
        LoadUnlockedItems();
    }

    void LoadUnlockedItems()
    {
        unlockedItems.Clear();

        foreach (int id in ProgressionManager.Instance.data.unlockedItemsIds)
        {
            Item item = itemDatabase.GetItemByID(id);
            if (item != null)
            {
                unlockedItems.Add(item);
            }
        }

        // np. uaktualnij UI
        Debug.Log("Za³adowano " + unlockedItems.Count + " odblokowanych przedmiotów.");
    }
}
