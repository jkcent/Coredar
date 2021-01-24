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

        // Stat Variables
        [SerializeField] int _health;
        [SerializeField] int _defence;
        [SerializeField] int _strength;
        [SerializeField] int _speed;
        [SerializeField] int _dodgeChance;
        [SerializeField] int _critChance;
        [SerializeField] int _critDamage;

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
            //
            _health = armor.health;
            _defence = armor.defence;
            _strength = armor.strength;
            _speed = armor.speed;
            _dodgeChance = armor.dodgeChance;
            _critChance = armor.critChance;
            _critDamage = armor.critDamage;
            //
            _prefab = armor.prefab;
        }

        public int health {
            get {
                return _health;
            }
            set {
                _health = value;
            }
        }
        public int defence {
            get {
                return _defence;
            }
            set {
                _defence = value;
            }
        }
        public int strength {
            get {
                return _strength;
            }
            set {
                _strength = value;
            }
        }
        public int speed {
            get {
                return _speed;
            }
            set {
                _speed = value;
            }
        }
        public int dodgeChance {
            get {
                return _dodgeChance;
            }
            set {
                _dodgeChance = value;
            }
        }
        public int critChance {
            get {
                return _critChance;
            }
            set {
                _critChance = value;
            }
        }
        public int critDamage {
            get {
                return _critDamage;
            }
            set {
                _critDamage = value;
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

            DisplayStats();
            DisplayEquipmentSlot();
            DisplayArmorType();
            DisplayPrefab();
        }

        public void DisplayStats() {
            EditorGUILayout.BeginHorizontal();
            _health = EditorGUILayout.IntField("Health", (int) _health);
            _defence = EditorGUILayout.IntField("Defence", (int) _defence);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            _critChance = EditorGUILayout.IntField("Crit Chance", (int) _critChance);
            _critDamage = EditorGUILayout.IntField("Crit Damage", (int) _critDamage);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            _speed = EditorGUILayout.IntField("Speed", (int) _speed);
            _dodgeChance = EditorGUILayout.IntField("Dodge Chance", (int) _dodgeChance);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            _strength = EditorGUILayout.IntField("Strength", (int) _strength);
            EditorGUILayout.EndHorizontal();
            /*
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            */
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
