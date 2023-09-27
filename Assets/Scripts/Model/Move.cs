using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Move {
    public string Name;
    public string Type;
    public string Category;
    public int PP;
    public int Power;
    public int Accuracy;
    public string Description;
    
    public MoveType moveType;
    public MoveCategory moveCategory;
    
    public Move(string name, string type, string category, int pp, int power, int accuracy, string description) {
        Name = name;
        Type =type;
        Category = category;
        PP = pp;
        Power = power;
        Accuracy = accuracy;
        Description = description;
    }
    
    public void SetEnums() {
        moveType = EnumSwitches.MoveTypePass(Type);
        moveCategory = EnumSwitches.MoveCategoryPass(Category);
    }

    public override string ToString() {
        return $"Name: {Name}, Type: {moveType}, Category: {moveCategory}, PP: {PP}, Power: {Power}, Accuracy: {Accuracy}, Description: {Description}";
    }
    
    
}
