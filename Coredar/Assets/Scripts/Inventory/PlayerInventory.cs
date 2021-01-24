using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coredar.ItemSystem;

[DisallowMultipleComponent]
public class PlayerInventory : MonoBehaviour {

    Inventory inventory;
    public GameObject itemSlotPrefab;
    [HideInInspector]
    public InventoryListWindow inventoryListWindow;
    #region Equipment definition
    [SerializeField] ISArmor helmet;
    [SerializeField] ISArmor chestplate;
    [SerializeField] ISArmor leggings;
    [SerializeField] ISArmor boots;
    [SerializeField] ISWeapon weapon;
    #endregion

    public PlayerInventory() {
        helmet = null;
        chestplate = null;
        leggings = null;
        boots = null;
        weapon = null;
        //
        inventory = new Inventory();
        //
        /*
        ISObject temp = new ISObject();
        temp.name = "Dame Tu Cosita";
        temp.quality = new ISQuality("AAH AAH", Color.red);
        inventory.AddItem(temp);
        */
    }

    public void SetWindowDetails() {
        inventoryListWindow.SetupList(inventory, itemSlotPrefab, new Vector2(-1, -1), new Vector2(0.5f, 0.5f), 0, 0, true);
    }

    public void EquipArmor(int slot) {
        ISArmor armorPiece = (ISArmor) inventory.GetItem(slot);
        if (armorPiece == null)
            return;

        ISArmor previousPiece = null;
        switch (armorPiece.equipmentSlot) {
            case EquipmentSlot.Head:
                previousPiece = helmet;
                helmet = armorPiece;
                break;
            case EquipmentSlot.Torso:
                previousPiece = chestplate;
                chestplate = armorPiece;
                break;
            case EquipmentSlot.Leg:
                previousPiece = leggings;
                leggings = armorPiece;
                break;
            case EquipmentSlot.Feet:
                previousPiece = boots;
                boots = armorPiece;
                break;
            default:
                break;
        }

        if (previousPiece != null)
            inventory.ReplaceItem(slot, previousPiece);

        inventoryListWindow.UpdateInventorySlots();
    }

    public void EquipWeapon(int slot) {
        ISWeapon newWeapon = (ISWeapon) inventory.GetItem(slot);
        if (newWeapon == null)
            return;

        ISWeapon previousWeapon = null;
        if (newWeapon.equipmentSlot == EquipmentSlot.Weapon) {
            previousWeapon = weapon;
            weapon = newWeapon;
        }

        if (previousWeapon != null)
            inventory.ReplaceItem(slot, previousWeapon);

        inventoryListWindow.UpdateInventorySlots();
    }

    public void AddItem(ISObject item) {
        inventory.AddItem(item);
        inventoryListWindow.UpdateInventorySlots();
    }

    public void DeleteItem(int slot) {
        inventory.RemoveItem(slot);
        inventoryListWindow.UpdateInventorySlots();
    }

    public ISObject GetItem(int slot) {
        return inventory.GetItem(slot);
    }

    public int GetSlotsFilled() {
        return inventory.inventory.Count;
    }
}
