using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Coredar.ItemSystem {
    [System.Serializable]
    public class ISObject : IISObject {

    [SerializeField] string _name;
    [SerializeField] int _value;
    [SerializeField] ISQuality _quality;
    [SerializeField] Sprite _icon;

        public string Name { 
            get { return _name; }
            set { _name = value; }
        }
        public int Value { 
            get { return _value; } 
            set { _value = value; } 
        }
        public ISQuality Quality { 
            get { return _quality; } 
            set { _quality = value; } 
        }
        public Sprite Icon { 
            get { return _icon; } 
            set { _icon = value; } 
        }

        public virtual void OnGUI() {
            GUILayout.BeginVertical();
            _name = EditorGUILayout.TextField("Name", _name);
            _value = EditorGUILayout.IntField("Value", _value);
            DisplayIcon();
            DisplayQuality();
            GUILayout.EndVertical();
        }

        public void DisplayIcon() {
            GUILayout.Label("Icon");
        }

        static ISQualityDatabase qdb;
        int selectedQualityIndex = 0;
        string[] options;

        public int SelectedQualityID {
            get {
                return selectedQualityIndex;
            }
        }

        public void OnEnable() {
            string FILE_NAME = @"coredarQualityDatabase.asset";
            string DATABASE_FOLDER_NAME = @"Database";
            qdb = ISQualityDatabase.GetDatabase<ISQualityDatabase>(DATABASE_FOLDER_NAME, FILE_NAME);
        }

        public ISObject() {
            
        }

        public void DisplayQuality() {
            if (qdb != null && options == null) {
                options = new string[qdb.Count];
                for (int i = 0; i < qdb.Count; i++) {
                    options[i] = qdb.Get(i).name;
                }
            }
            GUILayout.Label("Quality");
            if (options != null && qdb != null) {
            selectedQualityIndex = EditorGUILayout.Popup("Quality", selectedQualityIndex, options);
            _quality = qdb.Get(SelectedQualityID);
            }
        }
    }
}
