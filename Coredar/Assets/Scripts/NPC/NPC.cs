using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] enum NPCType {
    ShopKeeper  = 1,    // Owns Shop
    Quest       = 2,    // Gives Quests
    Dialog      = 3,    // Runs first interaction dialog and then generic dialog
    Generic     = 4,    // Only runs generic dialog
}

public class NPC : MonoBehaviour {

    [Header("| = \\n")]
    [SerializeField] NPCType npcType = NPCType.Dialog;
    public List<string> firstInteractionDialog = new List<string>();
    public List<string> genericDialog = new List<string>();
    public bool alreadyInteracted = false;

    public GameObject shop;
    //public GameObject quest;
    public GameObject dialog;

    public void StartInteraction() {
        //Debug.Log("Interacting With NPC");
        Settings.inNPCMenu = true;
        switch (npcType) {
            case NPCType.ShopKeeper:
                if (!alreadyInteracted) {
                    ShowFirstInteractionDialog();
                } else {
                    ShowGenericDialog();
                }
                break;
            case NPCType.Quest:
                if (!alreadyInteracted) {
                    ShowFirstInteractionDialog();
                } else {
                    ShowGenericDialog();
                }
                break;
            case NPCType.Dialog:
                if (!alreadyInteracted) {
                    ShowFirstInteractionDialog();
                } else {
                    ShowGenericDialog();
                }
                break;
            case NPCType.Generic:
                ShowGenericDialog();
                break;
        }
    }
    public void StopInteraction() {
        switch (npcType) {
            case NPCType.ShopKeeper:
                // Close Shop
                break;
            case NPCType.Quest:
                // Close Quest Menu
                break;
            case NPCType.Dialog:
                break;
            case NPCType.Generic:
                break;
        }
        Settings.inNPCMenu = false;
    }
    #region Dialog Functions
    void ShowFirstInteractionDialog() {
        dialog.SetActive(true);
        dialog.GetComponent<Dialog>().AccessCoroutine(firstInteractionDialog, this);
        alreadyInteracted = true;
    }
    void ShowGenericDialog() {
        dialog.SetActive(true);
        dialog.GetComponent<Dialog>().AccessCoroutine(genericDialog, this);
    }
    public void StopDialog() {
        dialog.SetActive(false);
        switch (npcType) {
            case NPCType.ShopKeeper:
                OpenShop();
                break;
            case NPCType.Quest:
                OpenQuest();
                break;
            case NPCType.Dialog:
            case NPCType.Generic:
                StopInteraction();
                break;
        }
    }
    #endregion
    #region NPC type functions
    void OpenShop() {
        shop.SetActive(true);
    }

    void OpenQuest() {

    }
    #endregion
}
