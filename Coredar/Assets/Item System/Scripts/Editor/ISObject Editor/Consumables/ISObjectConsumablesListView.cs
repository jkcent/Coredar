using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coredar.ItemSystem.Editor {
    public partial class ISObjectConsumables {
        ISConsumable _tempConsumable = new ISConsumable();

        Vector2 _scrollPos = Vector2.zero;
        int _selectedIndex = -1;
        bool createNewArmor = false;
        bool showDetails = false;

        public void ListView(int viewWidth, Vector2 buttonSize) {

            _scrollPos = GUILayout.BeginScrollView(_scrollPos, "Box", GUILayout.ExpandHeight(true), GUILayout.Width(viewWidth));

            for (int i = 0; i < db.Count; i++) {
                if (GUILayout.Button(db.Get(i).name, "Box", GUILayout.Width(buttonSize.x), GUILayout.Height(buttonSize.y))) {
                    _selectedIndex = i;
                    _tempConsumable = new ISConsumable(db.Get(i));
                    createNewArmor = true;
                    showDetails = true;
                }
            }

            GUILayout.EndScrollView();
        }
    }
}
