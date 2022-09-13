using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigasTools.Rpg{
    public static class LootTable
    {   
        public static Item GetDrop(Drop drop, int rolls){
            for (int i = 0; i < rolls; i++)
            {
                var r = drop.Rolled();
                if(!r)continue;
                BDebug.Log($"The object {drop.item.itemName} was rolled!", "Loot Table", Color.red);
                return drop.item;
            }
            return null;
        }
    }
}
