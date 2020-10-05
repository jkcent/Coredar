using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coredar.ItemSystem {
    public interface IISEquipable {

        ISEquipmentSlot equipmentSlot { get; }
        bool Equip();
    }
}
