using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Coredar.ItemSystem {
    [System.Serializable]
    public class ISObject : IISObject {

        [SerializeField] string _name;
        [SerializeField] int _value;
        [SerializeField] ISQuality _quality;
        [SerializeField] Sprite _icon;

        public ISObject() {
        }

        public ISObject(ISObject item) {
            Clone(item);
        }

        public void Clone(ISObject item) {
            _name = item.name;
            _value = item.value;
            _quality = item.quality;
            _icon = item.icon;
        }

        public string name {
            get { return _name; }
            set { _name = value; }
        }
        public int value { 
            get { return _value; } 
            set { _value = value; } 
        }
        public ISQuality quality { 
            get { return _quality; } 
            set { _quality = value; } 
        }
        public Sprite icon { 
            get { return _icon; } 
            set { _icon = value; } 
        }
        #if UNITY_EDITOR
        public virtual void OnGUI() {
            GUILayout.BeginVertical();
            _name = EditorGUILayout.TextField("Name", _name);
            _value = EditorGUILayout.IntField("Value", _value);
            DisplayIcon();
            DisplayQuality();
            GUILayout.EndVertical();
        }

        public void DisplayIcon() {
            _icon = EditorGUILayout.ObjectField("Icon", _icon, typeof(Sprite), false) as Sprite;
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

        public void DisplayQuality() {
            if (qdb != null) {
                options = new string[qdb.Count];
                for (int i = 0; i < qdb.Count; i++) {
                    options[i] = qdb.Get(i).name;
                }
            }
            if (options != null && qdb != null) {
                if (_quality == null)
                    _quality = qdb.Get(SelectedQualityID);

                selectedQualityIndex = EditorGUILayout.Popup("Quality", qdb.GetIndex(_quality.name), options);
                if (selectedQualityIndex >= 0)
                    _quality = qdb.Get(SelectedQualityID);
                else
                    _quality = qdb.Get(0);
            }
        }
        #endif
    }
}
