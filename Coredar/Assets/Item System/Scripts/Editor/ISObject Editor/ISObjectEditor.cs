#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Coredar.ItemSystem.Editor {
    public partial class ISObjectEditor : EditorWindow {

        ISObjectWeapon weaponDb = new ISObjectWeapon();
        ISObjectArmor armorDb = new ISObjectArmor();
        ISObjectConsumables consumablesDb = new ISObjectConsumables();

        [MenuItem("Coredar/Database/Item System Editor %#i")] // Open with shift-ctrl-w
        public static void init() {
            ISObjectEditor window = EditorWindow.GetWindow<ISObjectEditor>();
            window.minSize = new Vector2(800, 600);
            window.titleContent = new GUIContent("Item System");
            window.Show();
        }

        void OnEnable() {
            tabState = TabState.WEAPON;

            weaponDb.OnEnable();
            armorDb.OnEnable();
            consumablesDb.OnEnable();
        }

        int _listViewWidth = 200;
        int _listViewButtonWidth = 190;
        int _listViewButtonHeight = 25;

        void OnGUI() {
            TopTabBar();

            GUILayout.BeginHorizontal();
            switch (tabState) {
                case TabState.WEAPON:
                    weaponDb.ListView(_listViewWidth, new Vector2(_listViewButtonWidth, _listViewButtonHeight));
                    weaponDb.ItemDetails();
                    break;
                case TabState.ARMOR:
                    armorDb.ListView(_listViewWidth, new Vector2(_listViewButtonWidth, _listViewButtonHeight));
                    armorDb.ItemDetails();
                    break;
                case TabState.CONSUMABLES:
                    consumablesDb.ListView(_listViewWidth, new Vector2(_listViewButtonWidth, _listViewButtonHeight));
                    consumablesDb.ItemDetails();
                    break;
                default:
                    GUILayout.Label("About");
                    break;
            }
            GUILayout.EndHorizontal();
            BottomStatusBar();
        }
    }
}
#endif