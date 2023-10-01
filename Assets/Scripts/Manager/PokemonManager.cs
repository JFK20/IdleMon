using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PokemonManager : MonoBehaviour
{
    
    private Pokemon[] pokemons;
    
    public void Initialize()
    {
        pokemons = LoadPokemons();
    }

    private Pokemon[] LoadPokemons() {
        string path = Path.Join(Application.dataPath, "Data", "Pokemons.json");
        string jsonString = File.ReadAllText(path);
        jsonString = JsonHelper.FixJson(jsonString);
        Pokemon[] TMPpokemon = JsonHelper.FromJson<Pokemon>(jsonString);
        foreach (Pokemon pokemon in TMPpokemon) {
            Debug.Log(pokemon);
            pokemon.SetData();
        }
        return TMPpokemon;
    }
}
