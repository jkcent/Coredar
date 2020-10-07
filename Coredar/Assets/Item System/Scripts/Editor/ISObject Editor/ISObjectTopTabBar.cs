#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coredar.ItemSystem.Editor {
    public partial class ISObjectEditor {

        enum TabState {
            WEAPON,
            ARMOR,
            CONSUMABLES,
            ABOUT
        }

        TabState tabState;

        void TopTabBar() {
            GUILayout.BeginHorizontal("Box",GUILayout.ExpandWidth(true));

            WeaponsTab();
            ArmorTab();
            ConsumablesTab();
            AboutTab();

            GUILayout.EndHorizontal();
        }

        void WeaponsTab() {
            if (GUILayout.Button("Weapons"))
                tabState = TabState.WEAPON;
        }
        void ArmorTab() {
            if (GUILayout.Button("Armor"))
                tabState = TabState.ARMOR;
        }
        void ConsumablesTab() {
            if (GUILayout.Button("Consumables"))
                tabState = TabState.CONSUMABLES;
        }
        void AboutTab() {
            if (GUILayout.Button("About"))
                tabState = TabState.ABOUT;
        }
    } 
}
#endif