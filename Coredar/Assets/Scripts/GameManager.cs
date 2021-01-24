using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject menu;
    public PlayerInventory playerInventory;
    public GameObject inventoryList;

    GameObject activeInventory = null;
    void Update() {
        if (!Settings.paused) {
            if (Cursor.visible) {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        } else {
            if (!Cursor.visible) {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
        //
        // Open Menus
        if (Input.GetKeyDown(Settings.pauseKey)) {
            if (Settings.paused == menu.activeSelf) {
                menu.SetActive(!menu.activeSelf);
                Settings.paused = !Settings.paused;
            }
        } else if (Input.GetKeyDown(Settings.inventoryKey)) {
            /*
            if (Settings.paused == inventoryList.activeSelf) {
                inventoryList.SetActive(!inventoryList.activeSelf);
                inventoryList.SetActive(!inventoryList.activeSelf);
                Settings.paused = !Settings.paused;
            }
            */
            if (!Settings.paused && activeInventory == null) {
                activeInventory = Instantiate(inventoryList);
                activeInventory.transform.SetParent(GameObject.Find("GUI Components").transform, false);
                playerInventory.inventoryListWindow = activeInventory.GetComponent<InventoryListWindow>();
                playerInventory.SetWindowDetails();
                Settings.paused = true;
            } else if (Settings.paused && activeInventory != null) {
                Destroy(activeInventory);
                activeInventory = null;
                playerInventory.inventoryListWindow = null;
                Settings.paused = false;
            }
        }
    }

    public void UnpauseMenus() { // Unpauses any paused menus
        Settings.paused = false;
        menu.SetActive(false);
        //playerInventory.playerInventoryList.SetActive(false);
    }
}
