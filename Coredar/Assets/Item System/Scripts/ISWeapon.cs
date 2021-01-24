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

        // Weapon Variables
        [SerializeField] Vector2 _damage; // x is min y is max
        [SerializeField] int _attackRange;

        // Stat Variables
        [SerializeField] int _health;
        [SerializeField] int _defence;
        [SerializeField] int _strength;
        [SerializeField] int _speed;
        [SerializeField] int _dodgeChance;
        [SerializeField] int _critChance;
        [SerializeField] int _critDamage;

        // Object Variables
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
            //
            _damage = weapon.damage;
            _attackRange = weapon.attackRange;
            _health = weapon.health;
            _defence = weapon.defence;
            _strength = weapon.strength;
            _speed = weapon.speed;
            _dodgeChance = weapon.dodgeChance;
            _critChance = weapon.critChance;
            _critDamage = weapon.critDamage;
            //
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
        public int attackRange {
            get {
                return _attackRange;
            }
            set {
                _attackRange = value;
            }
        }

        // Stats

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

            EditorGUILayout.LabelField("");
            DisplayStats();
            EditorGUILayout.LabelField("");
            DisplayEquipmentSlot();
            DisplayWeaponType(); 
            DisplayPrefab();
        }

        public void DisplayStats() {
            EditorGUILayout.BeginHorizontal();
            _health = EditorGUILayout.IntField("Health", (int) _health);
            _defence = EditorGUILayout.IntField("Defence", (int) _defence);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            _damage.x = EditorGUILayout.IntField("Min Damage", (int) _damage.x);
            _damage.y = EditorGUILayout.IntField("Max Damage", (int) _damage.y);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            _critChance = EditorGUILayout.IntField("Crit Chance", (int) _critChance);
            _critDamage = EditorGUILayout.IntField("Crit Damage", (int) _critDamage);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            _strength = EditorGUILayout.IntField("Strength", (int) _strength);
            _attackRange = EditorGUILayout.IntField("Attack Range", (int) _attackRange);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            _speed = EditorGUILayout.IntField("Speed", (int) _speed);
            _dodgeChance = EditorGUILayout.IntField("Dodge Chance", (int) _dodgeChance);
            EditorGUILayout.EndHorizontal();
            /*
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            */
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
