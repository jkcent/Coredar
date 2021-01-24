using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Coredar.ItemSystem;

public class InventoryListWindow : MonoBehaviour {
    
    public GameObject itemSlotPrefab;
    public GameObject content;
    public ToggleGroup itemSlotToggleGroup;
    public Scrollbar scrollbar;

    public Inventory inventory = new Inventory();

    private int xPos = 0;
    private int yPos = 0;
    private GameObject itemSlot;

    public List<GameObject> activeSlots = new List<GameObject>();

    /*
    void OnEnable() {
        CreateInventorySlotsInWindow();
    }

    void OnDisable() {
        ClearGUIList();
    }
    */
    void CreateInventorySlotsInWindow() {
        for (int i = 0; i < inventory.GetSlotsFilled(); i++) {
            ISObject item = inventory.GetItem(i);
            #region Positioning and Organizing
            itemSlot = Instantiate(itemSlotPrefab);
            itemSlot.name = (i + 1).ToString();
            itemSlot.GetComponent<Toggle>().group = itemSlotToggleGroup;
            itemSlot.transform.SetParent(content.transform);

            itemSlot.GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x - 10, itemSlot.GetComponent<RectTransform>().sizeDelta.y);
            itemSlot.GetComponent<RectTransform>().localPosition = new Vector3(xPos, yPos, 0);

            yPos -= (int) itemSlot.GetComponent<RectTransform>().rect.height;

            itemSlot.GetComponent<Toggle>().onValueChanged.AddListener(delegate {
                ToggleClicked(itemSlot.GetComponent<Toggle>());
            });
            #endregion
            #region Details
            itemSlot.GetComponent<InventorySlotExecute>().Stylize(item);
            #endregion
            activeSlots.Add(itemSlot);
        }
    }

    void ClearGUIList() {
        foreach (GameObject listItem in activeSlots) {
            Destroy(listItem);
        }
        activeSlots.Clear();
    }

    bool scrollbarActive = false;
    private void Update() {
        
        if (Input.GetKeyDown(KeyCode.Return)) {
            ISObject temp = new ISObject();
            temp.name = "test";
            temp.quality = new ISQuality("test", Color.red);
            inventory.AddItem(temp, this);
        }
        
        if (scrollbarActive != scrollbar.gameObject.activeSelf)
            UpdateScrollbar();
    }

    public void UpdateInventorySlots() {
        if (gameObject.activeSelf) {
            ClearGUIList();
            CreateInventorySlotsInWindow();
        }
    }

    public void UpdateScrollbar() {
        if (scrollbar.gameObject.activeSelf) {
            GetComponentInChildren<ScrollRect>().movementType = ScrollRect.MovementType.Elastic;
        } else {
            GetComponentInChildren<ScrollRect>().movementType = ScrollRect.MovementType.Clamped;
        }

        scrollbarActive = scrollbar.gameObject.activeSelf;
    }

    public void ToggleClicked(Toggle toggle) {
        ISObject item = inventory.GetItem(int.Parse(toggle.gameObject.name) - 1);
        toggle.gameObject.GetComponent<InventorySlotExecute>().Execute(item);
    }

    public void SetupList(Inventory inventory, GameObject itemSlotPrefab, Vector2 size, Vector2 pivot, int x, int y, bool centered = false) {
        this.inventory = inventory;
        this.itemSlotPrefab = itemSlotPrefab;

        RectTransform rectTransform = GetComponent<RectTransform>();
        if (size.x == -1)
            size.x = rectTransform.sizeDelta.x;
        if (size.y == -1)
            size.y = rectTransform.sizeDelta.y;

        rectTransform.sizeDelta = size;
        rectTransform.pivot = pivot;

        if (centered)
            transform.localPosition += new Vector3(x, y);
        else
            transform.localPosition = new Vector3(x, y);

        CreateInventorySlotsInWindow();
    }
}