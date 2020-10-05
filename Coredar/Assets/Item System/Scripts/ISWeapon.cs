using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace Coredar.ItemSystem {
    [System.Serializable]
    public class ISWeapon : ISObject, IISWeapon, IISGameObject { // Maybe Destructable


        [SerializeField] int _minDamage;
        //[SerializeField] ISEquipmentSlot _equipmentSlot;
        [SerializeField] GameObject _prefab;

        public EquipmentSlot _equipmentSlot;

        public ISWeapon() {
            //_equipmentSlot = new ISEquipmentSlot();
        }

        public ISWeapon(GameObject prefab) {
            //_equipmentSlot = equipmentSlot;
            _prefab = prefab;
        }
        
        public int minDamage { 
            get {
                return _minDamage;
            }
            set {
                _minDamage = value;
            }
        }
        
        public int Attack() {
            throw new System.NotImplementedException();
        }
        
        public GameObject prefab { 
            get {
                return _prefab;
            }
        }

        public override void OnGUI() {
            base.OnGUI();

            _minDamage = EditorGUILayout.IntField("Damage", _minDamage);
            DisplayEquipmentSlot();
            DisplayPrefab();
        }

        public void DisplayEquipmentSlot() {
            _equipmentSlot =  (EquipmentSlot)EditorGUILayout.EnumPopup("Equipment Slot", _equipmentSlot);
        }

        public void DisplayPrefab() {
            _prefab = EditorGUILayout.ObjectField("Prefab", _prefab, typeof(GameObject), false) as GameObject;
        }
    }
}
