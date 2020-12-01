using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coredar.ItemSystem;

public class GameManager : MonoBehaviour {

    public ISWeaponDatabase weaponDb;
    public ISArmorDatabase armorDb;
    public ISConsumableDatabase consumableDb;

    //[HideInInspector]
    public static List<ISObject> itemList = new List<ISObject>();

    public void GenerateItemList() {
        itemList.Clear();

        for (int i = 0; i < weaponDb.Count; i++) {
            itemList.Add(weaponDb.Get(i));
        }
        for (int i = 0; i < armorDb.Count; i++) {
            itemList.Add(armorDb.Get(i));
        }
        for (int i = 0; i < consumableDb.Count; i++) {
            itemList.Add(consumableDb.Get(i));
        }

        for (int i = 0; i < itemList.Count; i++) {
            Debug.Log(itemList[i].name);
        }
    }
}
