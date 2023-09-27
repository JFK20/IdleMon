using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum MoveType {
    Fire,
    Water,
    Grass,
    Electric,
    Normal,
    Flying,
    Fighting,
    Poison,
    Ground,
    Rock,
    Bug,
    Ghost,
    Psychic,
    Ice,
    Dragon,
    Dark,
    Steel,
    Fairy,
}

public enum MoveCategory {
    Physical,
    Special,
    Status,
}

public static class EnumSwitches {

    public static MoveType MoveTypePass(string toPass) {
        try {
            MoveType value = (MoveType) Enum.Parse(typeof(MoveType), toPass, true);
            return value;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return MoveType.Fighting;
        }
    }
    
    public static MoveCategory MoveCategoryPass(string toPass) {
        try {
            MoveCategory value = (MoveCategory) Enum.Parse(typeof(MoveCategory), toPass, true);
            return value;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return MoveCategory.Special;
        }
    }
    
}
