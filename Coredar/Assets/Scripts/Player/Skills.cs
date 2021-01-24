using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Skills {
    #region Skills
    public static Skill playerLvl  = new Skill(1, 0, "Level",                                                       "*DO NOT SHOW DESCRIPTION* BIG\nHORSE\nCOCK\n...");
    public static Skill tracking   = new Skill(1, 0, "Tracking",                                "Track entites from farther distance and with higher stealth skills.");
    public static Skill detection  = new Skill(1, 0, "Detection",                                                                    "Locate higher rarity treasure.");
    public static Skill taming     = new Skill(1, 0, "Taming", "Tame higher level passive beasts.\nIf \"Beast Master\" perk is equipped hostile beasts can be tamed."); 
    public static Skill cooking    = new Skill(1, 0, "Cooking",                                                            "Allows you to cook higher quality foods.");
    public static Skill smithing   = new Skill(1, 0, "Smithing",                                "Create higher quality gear.\nAnalyze higher rarity unanalyzed gear.");
    public static Skill herbalist  = new Skill(1, 0, "Herbalist",                                           "Brew higher quality potions.\nGrow higher rarity crops.");
    public static Skill combat     = new Skill(1, 0, "Combat",                                                                  "Increases the player's damage stat.");
    public static Skill agility    = new Skill(1, 0, "Agility",                                         "Increases the player's speed stat.\nIncreases dodge chance.");
    public static Skill dungeoneer = new Skill(1, 0, "Dungeoneer",                                                           "The highest accessable dungeon to you.");
    public static Skill fishing    = new Skill(1, 0, "Fishing",                                                                          "Catch higher quality fish.");
    public static Skill magic      = new Skill(1, 0, "Magic",                                 "Increases the player's theiving stat.\nMakes enchanted gear stronger.");
    #endregion
}

public struct Skill {
    public int level { get; set; }
    public int exp { get; set; }
    public string name { get; }
    public string desc { get; }
    //
    public Skill(int level ,int exp, string name, string desc) {
        this.level = level;
        this.exp = exp;
        this.name = name;
        this.desc = desc;
    }
}

public static class Stats {
    public static Stat health       = new Stat("Health",          100);
    public static Stat invSpace     = new Stat("Inventory Space", 100);
    public static Stat defence      = new Stat("Defence",          20);
    public static Stat strength     = new Stat("Strength",          1);
    public static Stat speed        = new Stat("Speed",             1);
    public static Stat dodgeChance  = new Stat("Dodge Chance",      1);
    public static Stat theiving     = new Stat("Theiving",          1);
    public static Stat critChance   = new Stat("Crit Chance",       1);
    public static Stat trackingDist = new Stat("Stealth",           1);
}

public struct Stat {
    
    public string name { get; }
    public int baseValue { get; set; }
    public int finalValue { get; set; } // For effects like strength, etc. do damage.finalValue = damage.baseValue * multiplier or something along that line
    //
    public Stat(string name, int baseValue) {
        this.name = name;
        this.baseValue = baseValue;
        finalValue = baseValue;
    }
}