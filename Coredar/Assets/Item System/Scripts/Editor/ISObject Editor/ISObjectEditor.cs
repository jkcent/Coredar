using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Coredar.ItemSystem.Editor {
    public partial class ISObjectEditor : EditorWindow {

        ISWeaponDatabase db;

        const string FILE_NAME = @"coredarWeaponDatabase.asset";
        const string DATABASE_FOLDER_NAME = @"Database";
        //const string DATABASE_PATH = @"Assets/" + DATABASE_FOLDER_NAME + "/" + FILE_NAME;

        [MenuItem("Coredar/Database/Item System Editor %#i")] // Open with shift-ctrl-w
        public static void init() {
            ISObjectEditor window = EditorWindow.GetWindow<ISObjectEditor>();
            window.minSize = new Vector2(800, 600);
            window.titleContent = new GUIContent("Item System");
            window.Show();
        }

        void OnEnable() {
            if (db == null)
                db = ISWeaponDatabase.GetDatabase<ISWeaponDatabase>(DATABASE_FOLDER_NAME, FILE_NAME);

            _tempWeapon.OnEnable();
        }

        void OnGUI() {
            TopTabBar();
            GUILayout.BeginHorizontal();
            ListView();
            ItemDetails();
            GUILayout.EndHorizontal();
            BottomStatusBar();
        }
    }
}
