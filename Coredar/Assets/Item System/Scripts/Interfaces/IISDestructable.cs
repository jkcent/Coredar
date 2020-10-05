using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coredar.ItemSystem {
    public interface IISDestructable {
        int durability { get; }
        int maxDurability { get; }
        void TakeDamage(int amount);
        void Repair();
        void Break();
    }
}
