using UnityEngine;

public enum ItemType { Helmet, Armor, Pants, Boots, WeaponMain, WeaponOff, Amulet }

[System.Serializable]
public class Item
{
    public int id;
    public float damage;
    public string rarity;
    public string name;
    public string description;
    public Sprite icon;
    public ItemType itemType;
}
