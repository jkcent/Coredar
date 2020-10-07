using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Coredar.ItemSystem {
    [System.Serializable]
    public class ISArmor : ISObject, IISArmor, IISGameObject { // Maybe Destructable


        [SerializeField] int _defense;
        [SerializeField] GameObject _prefab;

        public EquipmentSlot equipmentSlot;
        public ArmorType armorType;

        public ISArmor() {
            equipmentSlot = EquipmentSlot.Feet;
        }

        public ISArmor(ISArmor armor) {
            Clone(armor);
        }

        public void Clone(ISArmor armor) {
            base.Clone(armor);
            equipmentSlot = armor.equipmentSlot;
            armorType = armor.armorType;
            _defense = armor.defense;
            _prefab = armor.prefab;
        }

        public int defense { 
            get { 
                return _defense; 
            } 
            set { 
                _defense = value; 
            }
        }

        public GameObject prefab {
            get {
                return _prefab;
            }
        }

        #if UNITY_EDITOR
        public override void OnGUI() {
            base.OnGUI();

            _defense = EditorGUILayout.IntField("Defense", _defense);
            DisplayEquipmentSlot();
            DisplayArmorType();
            DisplayPrefab();
        }

        public void DisplayEquipmentSlot() {
            equipmentSlot = (EquipmentSlot) EditorGUILayout.EnumPopup("Equipment Slot", equipmentSlot);
        }

        public void DisplayArmorType() {
            armorType = (ArmorType) EditorGUILayout.EnumPopup("Armor Type", armorType);
        }

        public void DisplayPrefab() {
            _prefab = EditorGUILayout.ObjectField("Prefab", _prefab, typeof(GameObject), false) as GameObject;
        }
        #endif
    }
}
