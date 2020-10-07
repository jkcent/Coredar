using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Coredar.ItemSystem.Editor {
    public partial class ISObjectConsumables {

        protected ISConsumableDatabase db { get; set; }

        const string dbName = @"coredarConsumablesDatabase.asset";
        const string dbPath = @"Database";

        public ISObjectConsumables() {

        }

        public string DatabaseFullPath {
            get {
                return @"Assets/" + dbPath + "/" + dbName;
            }
        }

        public void OnEnable() {
            if (db == null)
                db = ISConsumableDatabase.GetDatabase<ISConsumableDatabase>(dbPath, dbName);

            _tempConsumable.OnEnable();
        }
    }
}
