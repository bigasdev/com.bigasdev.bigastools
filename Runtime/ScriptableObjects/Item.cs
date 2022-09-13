using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Drops/Items")]
public class Item : ScriptableObject
{
    public string itemName = "Item";
    public string description = "Base Description";
    public int quantity = 1;
    public Sprite sprite;
}
[System.Serializable]
public class ItemDrop{
    public Drop[] drops;

    public ItemDrop(Drop[] drops)
    {
        this.drops = drops;
    }
}
[System.Serializable]
public class Drop{
    public Item item;
    public int chance = 16;

    public Drop(Item item, int chance)
    {
        this.item = item;
        this.chance = chance;
    }
    public bool Rolled(){
        var i = Random.Range(0, chance);
        return i == 1;
    }
}