using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Game/Item Database")]
public class ItemDatabase : ScriptableObject
{
    public List<Item> items;

    public Item GetItemByID(int id)
    {
        return items.Find(item => item.id == id);
    }
}
