using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum MoveType {
    Unknown,
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
    Unknown,
    Physical,
    Special,
    Status,
}

public enum GenderRatio {
    Unknown,
    AlwaysMale,
    FemaleOneEighth,
    Female25Percent,
    Female50Percent,
    Female75Percent,
    FemaleSevenEighths,
    AlwaysFemale,
    Genderless,
}

public enum Habitat {
    Unknown,
    Cave,
    Forest,
    Grassland,
    Mountain,
    Rare,
    RoughTerrain,
    Sea,
    Urban,
    WatersEdge,
}

public enum Stats {
    Unknown,
    HP,
    Attack,
    Defense,
    Speed,
    SpecialAttack,
    SpecialDefense,
}

public static class EnumSwitches {
    public static T EnumSwitch<T>(string toPass) {
        try {
            T value = (T) Enum.Parse(typeof(T), toPass, true);
            return value;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return default(T);
        }
    }
    
}
