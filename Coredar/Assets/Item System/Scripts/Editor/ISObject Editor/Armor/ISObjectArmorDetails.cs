using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Coredar.ItemSystem.Editor {
    public partial class ISObjectArmor {

        public void ItemDetails() {
            GUILayout.BeginVertical("Box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            GUILayout.BeginVertical(GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

            if (showDetails) {
                DisplayArmor();
            }
            
            GUILayout.EndVertical();
            GUILayout.Space(50);
            GUILayout.BeginHorizontal("Box", GUILayout.ExpandWidth(true));

            DisplayButtons();

            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }

        void DisplayArmor() {
            _tempArmor.OnGUI();
        }

        void DisplayButtons() {
            if (!createNewArmor) {
                if (GUILayout.Button("Create Armor")) {
                    _tempArmor = new ISArmor();
                    createNewArmor = true;
                    showDetails = true;
                }
            } else {
                GUI.SetNextControlName("SaveButton");

                if (GUILayout.Button("Save")) {
                    if (_selectedIndex == -1) {
                        db.Add(_tempArmor);
                    } else {
                        db.Replace(_selectedIndex, _tempArmor);
                    }

                    createNewArmor = false;
                    _tempArmor = null;
                    showDetails = false;
                    _selectedIndex = -1;
                    GUI.FocusControl("SaveButton");
                }

                if (_selectedIndex != -1) {
                    if (GUILayout.Button("Delete")) {
                        if (EditorUtility.DisplayDialog("Delete Armor", "Are you sure that you want to delete \"" + db.Get(_selectedIndex).name + "\"?", "Yes", "No")) {
                            db.RemoveAt(_selectedIndex);
                            createNewArmor = false;
                            _tempArmor = null;
                            showDetails = false;
                            _selectedIndex = -1;
                            GUI.FocusControl("SaveButton");
                        }
                    }
                }

                if (GUILayout.Button("Cancel")) {
                    createNewArmor = false;
                    _tempArmor = null;
                    showDetails = false;
                    _selectedIndex = -1;
                    GUI.FocusControl("SaveButton");
                }
            }
        }
    }
}
