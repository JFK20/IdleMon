using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MovesManager : MonoBehaviour
{
    
    private Move[] moves;
    
    private void Start() {
        LoadMoves();
    }

    private Move[] LoadMoves() {
        string path = Path.Join(Application.dataPath, "Data", "Moves.json");
        string jsonString = File.ReadAllText(path);
        jsonString = FixJson(jsonString);
        print(jsonString);
        Move[] TMPmoves = JsonHelper.FromJson<Move>(jsonString);
        foreach (var move in TMPmoves) {
            move.SetEnums();
        }
        Debug.Log(TMPmoves[0].ToString());

        return TMPmoves;
    }
    
    private string FixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }
    
}
