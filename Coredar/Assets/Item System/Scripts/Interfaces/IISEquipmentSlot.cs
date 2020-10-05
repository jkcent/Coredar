using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coredar.ItemSystem {
    public interface IISEquipmentSlot {
        string name { get; set; }
        Sprite icon { get; set; }
    }
}