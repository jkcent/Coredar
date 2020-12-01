using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Coredar.ItemSystem;

public class PlayerInventoryListWindow : MonoBehaviour {

    public GameObject itemSlotPrefab;
    public GameObject content;
    public ToggleGroup itemSlotToggleGroup;

    public PlayerInventory inventory;

    private int xPos = 0;
    private int yPos = 0;
    private GameObject itemSlot;

    public List<GameObject> activeSlots = new List<GameObject>();

    void OnEnable() {
        CreateInventorySlotsInWindow();
    }

    private void OnDisable() {
        ClearGUIList();
    }

    private void CreateInventorySlotsInWindow() {
        for (int i = 0; i < inventory.GetSlotsFilled(); i++) {
            ISObject item = inventory.GetItem(i);
            #region Positioning and Organizing
            itemSlot = Instantiate(itemSlotPrefab);
            itemSlot.name = (i + 1).ToString();
            itemSlot.GetComponent<Toggle>().group = itemSlotToggleGroup;
            itemSlot.transform.SetParent(content.transform);
            itemSlot.GetComponent<RectTransform>().localPosition = new Vector3(xPos, yPos, 0);
            yPos -= (int) itemSlot.GetComponent<RectTransform>().rect.height;
            #endregion
            #region Details
            Image[] images = itemSlot.GetComponentsInChildren<Image>();
            foreach (Image image in images) {
                GameObject attribute = image.gameObject;
                if (attribute.name == "Item Sprite") { // Check if working later
                    image.sprite = item.icon;
                    break;
                }
            }
            Text[] texts = itemSlot.GetComponentsInChildren<Text>();
            foreach (Text text in texts) {
                GameObject attribute = text.gameObject;
                if (attribute.name == "Item Name") {
                    text.text = item.name;
                } else if (attribute.name == "Item Rarity") {
                    text.text = item.quality.name;
                    text.color = item.quality.color;
                }
            }
            #endregion
            activeSlots.Add(itemSlot);
        }
    }

    private void ClearGUIList() {
        foreach (GameObject listItem in activeSlots) {
            Destroy(listItem);
        }
        activeSlots.Clear();
    }

    public void UpdateInventorySlots() {
        if (gameObject.activeSelf) {
            ClearGUIList();
            CreateInventorySlotsInWindow();
        }
    }
}
