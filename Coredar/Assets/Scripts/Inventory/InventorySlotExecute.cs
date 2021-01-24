using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Coredar.ItemSystem;

public enum SlotType {
    Default,
    Player,
    Store
}

public class InventorySlotExecute : MonoBehaviour {

    public SlotType slotType = SlotType.Default;

    public void Stylize(ISObject item) {
        switch (slotType) {
            #region Player
            case SlotType.Player:
                Image[] images = GetComponentsInChildren<Image>();
                foreach (Image image in images) {
                    GameObject attribute = image.gameObject;
                    if (attribute.name == "Item Sprite") { // Check if working later
                        image.sprite = item.icon;
                        break;
                    }
                }
                Text[] texts = GetComponentsInChildren<Text>();
                foreach (Text text in texts) {
                    GameObject attribute = text.gameObject;
                    if (attribute.name == "Item Name") {
                        text.text = item.name;
                    } else if (attribute.name == "Item Rarity") {
                        text.text = item.quality.name;
                        text.color = item.quality.color;
                    }
                }
                break;
            #endregion
            #region Store
            case SlotType.Store:
                images = GetComponentsInChildren<Image>();
                foreach (Image image in images) {
                    GameObject attribute = image.gameObject;
                    if (attribute.name == "Item Sprite") { // Check if working later
                        image.sprite = item.icon;
                        break;
                    }
                }
                texts = GetComponentsInChildren<Text>();
                foreach (Text text in texts) {
                    GameObject attribute = text.gameObject;
                    if (attribute.name == "Item Name") {
                        text.text = item.name;
                    } else if (attribute.name == "Item Rarity") {
                        text.text = item.quality.name;
                        text.color = item.quality.color;
                    }
                }
                break;
            #endregion
            default:
                break;
        }
    }

    public void Execute(ISObject item) {
        switch (slotType) {
            #region Player
            case SlotType.Player:
                break;
            #endregion
            #region Store
            case SlotType.Store:
                break;
            #endregion
            default:
                break;
        }
    }
}
