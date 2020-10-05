using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coredar.ItemSystem.Editor {
    public partial class ISObjectEditor {
        void TopTabBar() {
            GUILayout.BeginHorizontal("Box",GUILayout.ExpandWidth(true));

            WeaponsTab();
            ArmorTab();
            GUILayout.Button("Potions");
            AboutTab();

            GUILayout.EndHorizontal();
        }

        void WeaponsTab() {
            GUILayout.Button("Weapons");
        }
        void ArmorTab() {
            GUILayout.Button("Armor");
        }
        void AboutTab() {
            GUILayout.Button("About");
        }
    } 
}
