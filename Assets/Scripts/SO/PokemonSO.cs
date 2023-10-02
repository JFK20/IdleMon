using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/Pokemon", order = 1)] [Serializable]
public class PokemonSO : ScriptableObject
{
    [SerializeField]
    private string Name;
    [SerializeField]
    private int ID;
    [SerializeField]
    private List<MoveType> types = new List<MoveType>();
    [SerializeField] [Description("HP, Attack, Defense, Speed, Special Attack, Special Defense")]
    private List<int> baseStats; // HP, Attack, Defense, Speed, Special Attack, Special Defense
    [SerializeField]
    private GenderRatio genderRatio;
    [SerializeField]
    private string Ability;
    [SerializeField]
    private string HiddenAbilities;
    [SerializeField]
    private List<moveSetStruct> moveSetStructs;
    [SerializeField]
    private Habitat habitat;
    [SerializeField]
    private List<evolutionStruct> evolutionStructs;
    [SerializeField]
    private string Description;

    [SerializeField]
    private string Evolutions;
    
    [Serializable]
    public struct moveSetStruct {
        public Move move;
        public int level;
    }
        
    [Serializable]
    public struct evolutionStruct {
        public PokemonSO pokemon;
        public int level;
    }
        
    private Dictionary<Move, int> moveSet;
    private Dictionary<PokemonSO, int> evolutions;
    
    public void Initialize() {
        moveSet = new Dictionary<Move, int>();
        foreach (moveSetStruct moveSetStruct in moveSetStructs) {
            moveSet.Add(moveSetStruct.move, moveSetStruct.level);
        }
        
        evolutions = new Dictionary<PokemonSO, int>();
        foreach (evolutionStruct evolutionStruct in evolutionStructs) {
            evolutions.Add(evolutionStruct.pokemon, evolutionStruct.level);
        }
    }

    public string GetName() {
        return Name;
    }
    
    public void setAttributes(Pokemon pokemon) {
        Name = pokemon.Name;
        types = pokemon.GetTypes();
        baseStats = pokemon.GetBaseStats();
        genderRatio = pokemon.GetGenderRatio();
        Ability = pokemon.Ability;
        HiddenAbilities = pokemon.HiddenAbilities;
        moveSetStructs = pokemon.GetMoveSetStruct();
        habitat = pokemon.GetHabitat();
        Evolutions = pokemon.Evolutions;
        Description = pokemon.Description;

    }
    
    public void SetEvolutions(List<evolutionStruct> evolutionStruct) {
        this.evolutionStructs = evolutionStruct;
    }

    public List<evolutionStruct> InitEvolutions() {
        string evolutionsString = Evolutions;
        string[] TMPEvolutions = evolutionsString.Split(",");
        List<evolutionStruct> TMPEvolutionsList = new List<evolutionStruct>();
        if (TMPEvolutions.Length > 2) {
            for (int i = 0; i < TMPEvolutions.Length; i += 3) {
                try {
                    Debug.Log(TMPEvolutions[i] + ": " + TMPEvolutions[i + 2]);
                    PokemonSO tmpPokemon = Resources.Load<PokemonSO>("SO/" + TMPEvolutions[i]);
                    Debug.Log(tmpPokemon);
                    int tmpLevel = int.Parse(TMPEvolutions[i + 2]);
                    evolutionStruct tmpStruct = new evolutionStruct();
                    tmpStruct.pokemon = tmpPokemon;
                    tmpStruct.level = tmpLevel;
                    TMPEvolutionsList.Add(tmpStruct);
                }
                catch (Exception e) {
                    Debug.Log("Evolution not with Level: " + e);
                }
            }
        }

        return TMPEvolutionsList;
    }

    public void DumpToJson() {
        string path = Path.Join(Application.dataPath, "Resources", "pokemons.json");
        StreamWriter writer = new StreamWriter(path, true);
        writer.Write(JsonUtility.ToJson(this));
        writer.Close();
    }
}

