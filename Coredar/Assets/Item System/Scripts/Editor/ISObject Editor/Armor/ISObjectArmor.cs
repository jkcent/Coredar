using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Coredar.ItemSystem.Editor {
    public partial class ISObjectArmor {

        protected ISArmorDatabase db { get; set; }

        const string dbName = @"coredarArmorDatabase.asset";
        const string dbPath = @"Database";

        public ISObjectArmor() {
            
        }

        public string DatabaseFullPath {
            get {
                return @"Assets/" + dbPath + "/" + dbName;
            }
        }

        public void OnEnable() {
            if (db == null)
                db = ISArmorDatabase.GetDatabase<ISArmorDatabase>(dbPath, dbName);

            _tempArmor.OnEnable();
        }
    }
}
