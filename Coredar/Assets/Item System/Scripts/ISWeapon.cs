using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Coredar.ItemSystem {
    [System.Serializable]
    public class ISWeapon : ISObject, IISWeapon, IISGameObject { // Maybe Destructable


        [SerializeField] Vector2 _damage; // x is min y is max
        [SerializeField] GameObject _prefab;

        public EquipmentSlot equipmentSlot;
        public WeaponType weaponType;

        public ISWeapon() {
            equipmentSlot = EquipmentSlot.Weapon;
        }

        public ISWeapon(ISWeapon weapon) {
            Clone(weapon);
        }
        
        public void Clone(ISWeapon weapon) {
            base.Clone(weapon);
            equipmentSlot = weapon.equipmentSlot;
            weaponType = weapon.weaponType;
            _damage = weapon.damage;
            _prefab = weapon.prefab;
        }

        public Vector2 damage { 
            get {
                return _damage;
            }
            set {
                _damage = value;
            }
        }
        
        public void Attack() {
            throw new System.NotImplementedException();
        }

        public GameObject prefab { 
            get {
                return _prefab;
            }
        }
        #if UNITY_EDITOR
        public override void OnGUI() {
            base.OnGUI();

            DisplayDamage();
            DisplayEquipmentSlot();
            DisplayWeaponType(); 
            DisplayPrefab();
        }

        public void DisplayDamage() {
            _damage.x = EditorGUILayout.IntField("Min Damage", (int) _damage.x);
            _damage.y = EditorGUILayout.IntField("Max Damage", (int) _damage.y);
        }

        public void DisplayEquipmentSlot() {
            equipmentSlot = (EquipmentSlot)EditorGUILayout.EnumPopup("Equipment Slot", equipmentSlot);
        }

        public void DisplayWeaponType() {
            weaponType = (WeaponType)EditorGUILayout.EnumPopup("Weapon Type", weaponType);
        }

        public void DisplayPrefab() {
            _prefab = EditorGUILayout.ObjectField("Prefab", _prefab, typeof(GameObject), false) as GameObject;
        }
        #endif
    }
}
