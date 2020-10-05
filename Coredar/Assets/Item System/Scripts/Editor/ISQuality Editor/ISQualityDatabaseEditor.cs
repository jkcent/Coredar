using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Coredar.ItemSystem.Editor {
    public partial class ISQualityDatabaseEditor : EditorWindow {

        ISQualityDatabase db;

        Vector2 _scrollPos;

        const string FILE_NAME = @"coredarQualityDatabase.asset";
        const string DATABASE_FOLDER_NAME = @"Database";
        //const string DATABASE_PATH = @"Assets/" + DATABASE_FOLDER_NAME + "/" + FILE_NAME;

        [MenuItem("Coredar/Database/Quality Editor %#w")] // Open with shift-ctrl-i
        public static void init() {
            ISQualityDatabaseEditor window = EditorWindow.GetWindow<ISQualityDatabaseEditor>();
            window.minSize = new Vector2(400, 300);
            window.titleContent = new GUIContent("Quality Database");
            window.Show();
        }

        void OnEnable() {
            if (db == null)
                db = ISQualityDatabase.GetDatabase<ISQualityDatabase>(DATABASE_FOLDER_NAME, FILE_NAME);
        }

        void OnGUI() {
            if (db == null) {
                Debug.LogWarning("Quality Database not loaded.");
                return;
            }

            ListView();

            GUILayout.BeginHorizontal("Box", GUILayout.ExpandWidth(true));
            BottomBar();
            GUILayout.EndHorizontal();
        }

        void BottomBar() {
            GUILayout.Label("Qualities: " + db.Count);

            if (GUILayout.Button("Add")) {
                db.Add(new ISQuality());
            }
            if (GUILayout.Button("Save")) {
                db.Save();
            }
        }
    }
}
