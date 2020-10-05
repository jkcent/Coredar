using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coredar.ItemSystem {
    public interface IISObject {
        /* IS means Item Sysstem */
        string Name { get; set; }
        int Value { get; set; } // if < 0 can't sell
        ISQuality Quality { get; set; }
        Sprite Icon { get; set; }

        // These go to other item interfaces
        // questItem Flag
        // lore
    }
}
