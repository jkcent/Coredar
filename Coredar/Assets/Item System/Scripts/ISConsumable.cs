using Coredar.ItemSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Coredar.ItemSystem {
    [System.Serializable]
    public class ISConsumable : ISObject, IISConsumable {

        [SerializeField] int _consumptionTime;

        public ISConsumable() {

        }

        public int consumptionTime { 
            get {
                return _consumptionTime;
            } 
            set {
                _consumptionTime = value;
            } 
        }

        public ISConsumable(ISConsumable consumable) {
            Clone(consumable);
        }

        public void Clone(ISConsumable consumable) {
            base.Clone(consumable);
            _consumptionTime = consumable.consumptionTime;
        }

        #if UNITY_EDITOR
        public override void OnGUI() {
            base.OnGUI();

            _consumptionTime = EditorGUILayout.IntField("Consumption Time", _consumptionTime);
        }
        #endif
    }
}
