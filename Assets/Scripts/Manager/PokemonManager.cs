using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class PokemonManager : MonoBehaviour
{
    private Pokemon[] pokemons;
    private List<PokemonSO> pokemonSOs;
    
    public void Initialize()
    {
        pokemons = LoadPokemons();
        pokemonSOs = CreateSo();
        PrintPokemon();
    }

    private Pokemon[] LoadPokemons() {
        string path = Path.Join(Application.dataPath, "Data", "Pokemons.json");
        string jsonString = File.ReadAllText(path);
        jsonString = JsonHelper.FixJson(jsonString);
        Pokemon[] TMPpokemon = JsonHelper.FromJson<Pokemon>(jsonString);
        foreach (Pokemon pokemon in TMPpokemon) {
            pokemon.SetData();
        }
        return TMPpokemon;
    }

    private List<PokemonSO> CreateSo() {
        List<PokemonSO> Sotmp = new List<PokemonSO>();
        foreach (var pokemon in pokemons) {
            Sotmp.Add(CreatePokemonSO.Spawn(pokemon));
        }
        
        /*for (int i = 0; i < 20; i++) {
            Sotmp.Add(CreatePokemonSO.Spawn(pokemons[i]));
            
        }*/

        return Sotmp;

    }
    
    public Pokemon GetPokemonByName(string pokemonName) {
        foreach (Pokemon pokemon in pokemons) {
            pokemonName = pokemonName.Replace(" ", "");
            string tmppokemon = pokemon.Name.Replace(" ", "");
            if (string.Equals(tmppokemon, pokemonName, StringComparison.CurrentCultureIgnoreCase)) {
                return pokemon;
            }
        }
        return null;
    }
    
    public void PrintPokemon() {
        PokemonSO[] test = Resources.LoadAll<PokemonSO>("SO");
        foreach (var poke in test) {
            Debug.Log(poke);
            poke.SetEvolutions(poke.InitEvolutions());
            poke.DumpToJson();
        }
    }
}
