using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour
{
    public ItemDatabase itemDatabase;
    public Playerscript player;

    [Header("Inventory UI")]
    public Transform inventoryPanel; // np. InventoryPanel (w Canvas)
    public GameObject inventorySlotPrefab; // prefab with image component

    private List<Item> unlockedItems = new List<Item>();
    void Start()
    {
        LoadUnlockedItems();
        ApplyItemEffectsToPlayer();
        DisplayInventoryUI();
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
    public void DisplayInventoryUI()
    {
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject); // Clearing old icons
        }

        foreach (Item item in unlockedItems)
        {
            GameObject slot = Instantiate(inventorySlotPrefab, inventoryPanel);

            Transform iconTransform = slot.transform.Find("Icon");

            Image image = slot.GetComponent<Image>();


            if (iconTransform != null)
            {
                Image iconImage = iconTransform.GetComponent<Image>();

                if (iconImage != null && item.icon != null)
                {
                    iconImage.sprite = item.icon;
                    iconImage.color = Color.white;
                }
            }
        }
    }
}
