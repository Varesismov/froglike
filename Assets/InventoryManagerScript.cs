using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public ItemDatabase itemDatabase;

    private List<Item> unlockedItems = new List<Item>();
    //void OnEnable()
    //{
    //    StartCoroutine(DelayedLoad());
    //}

    //IEnumerator DelayedLoad()
    //{
    //    yield return new WaitUntil(() => ProgressionManager.Instance != null && ProgressionManager.Instance.data != null);
    //    LoadUnlockedItems();
    //}

    void Start()
    {
        LoadUnlockedItems();
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
                Debug.Log("Odblokowany przedmiot: " + item.name);
            }
        }

        // np. uaktualnij UI
        Debug.Log("Za³adowano " + unlockedItems.Count + " odblokowanych przedmiotów.");
    }
}
