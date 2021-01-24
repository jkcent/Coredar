using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] enum NPCType {
    ShopKeeper,    // Owns Shop
    Quest,         // Gives Quests
    Dialog,        // Runs first interaction dialog and then generic dialog
    Generic,       // Only runs generic dialog
}

public class NPC : MonoBehaviour {

    [Header("| = \\n")]
    [SerializeField] NPCType npcType = NPCType.Dialog;
    public List<string> firstInteractionDialog = new List<string>();
    public List<string> genericDialog = new List<string>();
    public bool alreadyInteracted = false;

    //public GameObject quest;
    public GameObject dialog;

    public void StartInteraction() {
        //Debug.Log("Interacting With NPC");
        Settings.paused = true;
        switch (npcType) {
            case NPCType.ShopKeeper:
                if (!alreadyInteracted) {
                    ShowFirstInteractionDialog(); // Goes to Menu
                } else {
                    ShowGenericDialog(); // For second Interaction
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
        Settings.paused = false;
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
        GetComponent<Store>().CreateList();
    }

    void OpenQuest() {

    }
    #endregion
}
