using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Coredar.ItemSystem.Editor {
    public partial class ISObjectConsumables {

        public void ItemDetails() {
            GUILayout.BeginVertical("Box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            GUILayout.BeginVertical(GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

            if (showDetails) {
                DisplayConsumables();
            }

            GUILayout.EndVertical();
            GUILayout.Space(50);
            GUILayout.BeginHorizontal("Box", GUILayout.ExpandWidth(true));

            DisplayButtons();

            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }

        void DisplayConsumables() {
            _tempConsumable.OnGUI();
        }

        void DisplayButtons() {
            if (!createNewArmor) {
                if (GUILayout.Button("Create Consumable")) {
                    _tempConsumable = new ISConsumable();
                    createNewArmor = true;
                    showDetails = true;
                }
            } else {
                GUI.SetNextControlName("SaveButton");

                if (GUILayout.Button("Save")) {
                    if (_selectedIndex == -1) {
                        db.Add(_tempConsumable);
                    } else {
                        db.Replace(_selectedIndex, _tempConsumable);
                    }

                    createNewArmor = false;
                    _tempConsumable = null;
                    showDetails = false;
                    _selectedIndex = -1;
                    GUI.FocusControl("SaveButton");

                    //GameManager.GenerateItemList();
                    GameManager manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
                    if (manager == null) {
                        Debug.Log("GameManager does not exist");
                    } else {
                        manager.GenerateItemList();
                    }
                }

                if (_selectedIndex != -1) {
                    if (GUILayout.Button("Delete")) {
                        if (EditorUtility.DisplayDialog("Delete Consumable", "Are you sure that you want to delete \"" + db.Get(_selectedIndex).name + "\"?", "Yes", "No")) {
                            db.RemoveAt(_selectedIndex);
                            createNewArmor = false;
                            _tempConsumable = null;
                            showDetails = false;
                            _selectedIndex = -1;
                            GUI.FocusControl("SaveButton");

                            //GameManager.GenerateItemList();
                            GameManager manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
                            if (manager == null) {
                                Debug.Log("GameManager does not exist");
                            } else {
                                manager.GenerateItemList();
                            }
                        }
                    }
                }

                if (GUILayout.Button("Cancel")) {
                    createNewArmor = false;
                    _tempConsumable = null;
                    showDetails = false;
                    _selectedIndex = -1;
                    GUI.FocusControl("SaveButton");
                }
            }
        }
    }
}
