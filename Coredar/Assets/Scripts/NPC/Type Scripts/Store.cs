using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Coredar.ItemSystem;

public class Store : MonoBehaviour {

    public int[] itemIndexes;
    public List<ISObject> inventory = new List<ISObject>();

    public ToggleGroup shopSlotToggleGroup;
    public GameObject shopSlotPrefab;
    public GameObject content;
    private GameObject shopSlot;

    int xPos = 0;
    int yPos = 0;

    void OnEnable() {
        Settings.inNPCMenu = true;
        GenerateInventory();
        CreateInventorySlotsInWindow();
    }

    void OnDisable() {
        Settings.inNPCMenu = false;
    }

    void GenerateInventory() {
        inventory.Clear();

        for (int i = 0; i < itemIndexes.Length; i++) {
            if (itemIndexes[i] < GameManager.staticItemList.Count && itemIndexes[i] >= 0) {
                inventory.Add(GameManager.staticItemList[itemIndexes[i]]);
            }
        }
    }

    void CreateInventorySlotsInWindow() {
        for (int i = 0; i < inventory.Count; i++) {
            ISObject item = inventory[i];
            #region Positioning and Organizing
            shopSlot = Instantiate(shopSlotPrefab);
            shopSlot.name = (i + 1).ToString();
            shopSlot.GetComponent<Toggle>().group = shopSlotToggleGroup;
            shopSlot.transform.SetParent(content.transform);
            shopSlot.GetComponent<RectTransform>().localPosition = new Vector3(xPos, yPos, 0);
            yPos -= (int) shopSlot.GetComponent<RectTransform>().rect.height;
            #endregion
            #region Details
            Image[] images = shopSlot.GetComponentsInChildren<Image>();
            foreach (Image image in images) {
                GameObject attribute = image.gameObject;
                if (attribute.name == "Item Sprite") { // Check if working later
                    image.sprite = item.icon;
                    break;
                }
            }
            Text[] texts = shopSlot.GetComponentsInChildren<Text>();
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
        }
    }
}
