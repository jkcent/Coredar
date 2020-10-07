using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coredar.ItemSystem;

[DisallowMultipleComponent]
public class Weapon : MonoBehaviour {
    public int value;
    public ISQuality quality;
    public Sprite icon;
    public Vector2 damage;
    public EquipmentSlot equipmentSlot;
    public WeaponType weaponType;
}
