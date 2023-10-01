using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MovesManager : MonoBehaviour {

    [SerializeField]
    private PokemonManager pokemonManager;
    
    private Move[] moves;
    
    private void Start() {
        moves = LoadMoves();
        pokemonManager.Initialize();
    }

    private Move[] LoadMoves() {
        string path = Path.Join(Application.dataPath, "Data", "Moves.json");
        string jsonString = File.ReadAllText(path);
        jsonString = JsonHelper.FixJson(jsonString);
        Move[] TMPmoves = JsonHelper.FromJson<Move>(jsonString);
        foreach (var move in TMPmoves) {
            move.SetEnums();
        }
        //Debug.Log(TMPmoves[0]);

        return TMPmoves;
    }

    public Move GetMoveByName(string moveName) {
        foreach (Move move in moves) {
            moveName = moveName.Replace(" ", "");
            string tmpmove = move.Name.Replace(" ", "");
            if (string.Equals(tmpmove, moveName, StringComparison.CurrentCultureIgnoreCase)) {
                return move;
            }
        }
        return null;
    }
    
}
