using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coredar.ItemSystem {
    public interface IISObject {
        /* IS means Item Sysstem */
        string name { get; set; }
        int value { get; set; } // if < 0 can't sell
        ISQuality quality { get; set; }
        Sprite icon { get; set; }

        // These go to other item interfaces
        // questItem Flag
        // lore
    }
}
