using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coredar.ItemSystem;

[System.Serializable]
public class GameManager : MonoBehaviour {

    public ISWeaponDatabase weaponDb;
    public ISArmorDatabase armorDb;
    public ISConsumableDatabase consumableDb;

    //[HideInInspector]
    public List<ISObject> itemList = new List<ISObject>();
    public static List<ISObject> staticItemList = new List<ISObject>();

    void Awake() {
        staticItemList = itemList;
        //Debug.Log(staticItemList.Count);
    }

    void Update() {
        if (staticItemList != itemList) {
            staticItemList = itemList;
            //Debug.Log(staticItemList.Count);
        }
    }

    public void GenerateItemList() {
        List<ISObject> tempList = itemList;

        List<int> usedIndexes = new List<int>();

        for (int i = 0; i < weaponDb.Count; i++) {
            if (!tempList.Contains(weaponDb.Get(i))) {
                tempList.Add(weaponDb.Get(i));
            }
            usedIndexes.Add(tempList.IndexOf(weaponDb.Get(i)));
        }
        for (int i = 0; i < armorDb.Count; i++) {
            if (!tempList.Contains(armorDb.Get(i))) {
                tempList.Add(armorDb.Get(i));
            }
            usedIndexes.Add(tempList.IndexOf(armorDb.Get(i)));
        }
        for (int i = 0; i < consumableDb.Count; i++) {
            if (!tempList.Contains(consumableDb.Get(i))) {
                tempList.Add(consumableDb.Get(i));
            }
            usedIndexes.Add(tempList.IndexOf(consumableDb.Get(i)));
        }

        List<int> removedIndexes = new List<int>();
        for (int i = 0; i < tempList.Count; i++) {
            if (!usedIndexes.Contains(i))
                removedIndexes.Add(i);
        }

        for (int i = 0; i < removedIndexes.Count; i++) {
            tempList.RemoveAt(removedIndexes[i]);
        }

        itemList = tempList;

        Debug.Log(staticItemList.Count);
        /*
        for (int i = 0; i < itemList.Count; i++) {
            Debug.Log(itemList[i].name);
        }
        */
    }
}
