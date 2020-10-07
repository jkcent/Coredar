using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Coredar.ItemSystem.Editor {
    public partial class ISQualityDatabaseEditor {

        // List all of the stored qualities in the database
        void ListView() {
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.ExpandHeight(true));
                DisplayQualities();
            EditorGUILayout.EndScrollView();
        }

        void DisplayQualities() {
            for (int count = 0; count < db.Count; count++) {
                GUILayout.BeginHorizontal("Box");
                db.Get(count).name = EditorGUILayout.TextField(db.Get(count).name);
                db.Get(count).color = EditorGUILayout.ColorField(db.Get(count).color);
                if (GUILayout.Button("Delete", GUILayout.Width(60))) {
                    if (EditorUtility.DisplayDialog("Delete Quality", "Are you sure that you want to delete \"" + db.Get(count).name + "\"?", "Yes", "No")) {
                        db.RemoveAt(count);
                    }
                }
                GUILayout.EndHorizontal();
            }
        }
    }
}
