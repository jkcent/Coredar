using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coredar.ItemSystem.Editor {
    public partial class ISObjectEditor {
        Vector2 _scrollPos = Vector2.zero;
        int _listViewWidth = 200;

        void ListView() {
            _scrollPos = GUILayout.BeginScrollView(_scrollPos, "Box", GUILayout.ExpandHeight(true), GUILayout.Width(_listViewWidth)) ;

            GUILayout.Label("List View");

            GUILayout.EndScrollView();
        }
    }
}
