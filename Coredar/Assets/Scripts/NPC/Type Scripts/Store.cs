using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Coredar.ItemSystem;

public class Store : MonoBehaviour {

    [Header("----- Databases -----")]
    public ISWeaponDatabase weaponDb;
    public ISArmorDatabase armorDb;
    public ISConsumableDatabase consumablesDb;
    [Header("----- Lists -----")]
    public List<int> weaponIndexes = new List<int>();
    public List<int> armorIndexes = new List<int>();
    public List<int> consumablesIndexes = new List<int>();
    public Inventory weaponInventory = new Inventory();
    public Inventory armorInventory = new Inventory();
    public Inventory consumablesInventory = new Inventory();
    [Header("----- Prefabs -----")]
    public GameObject blankList;
    public GameObject itemSlot;

    void Update() {
        if (Input.GetKeyDown(Settings.accessKey)) {
            //gameObject.SetActive(false);
        }
    }

    GameObject weaponObject;
    GameObject armorObject;
    GameObject consumablesObject;

    public void Awake() {
        GenerateInventory();
    }

    public void CreateList() {
        if (blankList == null)
            return;

        int openInventories = 0;

        if (weaponInventory.GetSlotsFilled() > 0) {
            weaponObject = Instantiate(blankList);
            openInventories++;
        }
        if (armorInventory.GetSlotsFilled() > 0) {
            armorObject = Instantiate(blankList);
            openInventories++;
        }
        if (consumablesInventory.GetSlotsFilled() > 0) {
            consumablesObject = Instantiate(blankList);
            openInventories++;
        }

        Transform GUIComponents = GameObject.Find("GUI Components").transform;

        if (weaponInventory.GetSlotsFilled() > 0) {
            weaponObject.name = "Weapon Store";
            //weaponObject.transform.SetParent(GUIComponents);
            weaponObject.GetComponent<InventoryListWindow>().SetupList(weaponInventory, itemSlot, new Vector2(300, -1), new Vector2(0.5f, 0.5f), 0, 0, true);
        }
        if (armorInventory.GetSlotsFilled() > 0) {

        }
        if (consumablesInventory.GetSlotsFilled() > 0) {

        }
    }

    void GenerateInventory() {
        weaponInventory.Clear();
        armorInventory.Clear();
        consumablesInventory.Clear();

        for (int i = 0; i < weaponDb.Count; i++) {
            if (weaponIndexes.Contains(i))
                weaponInventory.AddItem(weaponDb.Get(i));
        }
        for (int i = 0; i < armorDb.Count; i++) {
            if (armorIndexes.Contains(i))
                armorInventory.AddItem(armorDb.Get(i));
        }
        for (int i = 0; i < consumablesDb.Count; i++) {
            if (consumablesIndexes.Contains(i))
                consumablesInventory.AddItem(consumablesDb.Get(i));
        }
    }
}