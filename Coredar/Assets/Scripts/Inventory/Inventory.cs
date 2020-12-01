using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coredar.ItemSystem;

public class Inventory { // Used for player and storage

    //ISObject[] inventory;
    public List<ISObject> inventory;

    public Inventory () {
        inventory = new List<ISObject>();
        ISObject temp = new ISObject();
        temp.name = "test";
        temp.quality = new ISQuality("test", Color.red);
        inventory.Add(temp);
    }

    public void AddItem(ISObject item) {
        if (inventory.Count < Stats.invSpace.finalValue) {
            inventory.Add(item);
            Debug.Log($"{item.quality.name} {item.name} added to slot {inventory.Count}.");
        } else {
            Debug.Log($"{inventory.Count} / {inventory.Count} slots filled.\nInventory Full.");
        }
    }

    public void RemoveItem(int slot) {
        if (inventory.Count > 0 && slot >= 0 && slot <= inventory.Count) {
            Debug.Log($"Removed {inventory[slot - 1].quality.name} {inventory[slot - 1].name} from slot {slot}");
            inventory.RemoveAt(slot - 1);
        } else if (slot < 0 || slot > Stats.invSpace.finalValue) {
            Debug.Log($"Slot {slot} does not exist.");
        } else if (slot > inventory.Count) {
            Debug.Log($"Slot {slot} is already empty.");
        }
    }

    public ISObject GetItem(int slot) {
        return inventory[slot];
    }

    public void ReplaceItem(int slot, ISObject item) {
        if (inventory[slot] != null) {
            inventory[slot] = item;
        } else {
            AddItem(item);
        }
    }
}
