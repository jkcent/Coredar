using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coredar.ItemSystem {
    public class ISEquipmentSlot : IISEquipmentSlot {

        [SerializeField] string _name;
        [SerializeField] Sprite _icon;
        [SerializeField] Texture2D _tex = new Texture2D(0, 0);

        public ISEquipmentSlot() {
            _name = "Name Me";
            _icon = Sprite.Create(_tex, new Rect(0f, 0f, 0f, 0f), Vector2.zero);
        }

        public string name { 
            get { return _name; } 
            set { _name = value; } 
        }
        public Sprite icon { 
            get { return _icon; } 
            set { _icon = value; }
        }
    }
}
