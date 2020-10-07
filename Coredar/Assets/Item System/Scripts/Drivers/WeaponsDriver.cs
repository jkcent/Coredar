using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coredar.ItemSystem;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class WeaponsDriver : MonoBehaviour {

    public ISWeaponDatabase database;
    public Text testText;

    void OnGUI() {
        for (int i = 0; i < database.Count; i++) {
            if (GUILayout.Button("Spawn " + database.Get(i).name)) {
                Spawn(i);
            }
        }
    }

    void Spawn(int index) {
        ISWeapon isw = database.Get(index);

        GameObject weapon = Instantiate(isw.prefab);
        weapon.name = isw.name;

        Weapon myWeapon = weapon.AddComponent<Weapon>();
        myWeapon.value = isw.value;
        myWeapon.quality = isw.quality;
        myWeapon.icon = isw.icon;
        myWeapon.damage = isw.damage;
        myWeapon.equipmentSlot = isw.equipmentSlot;
        myWeapon.weaponType = isw.weaponType;
    }
}
