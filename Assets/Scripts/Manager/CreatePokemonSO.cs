using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class CreatePokemonSO : MonoBehaviour
{
    [MenuItem("SO/Pokemons")]
    public static PokemonSO Spawn(Pokemon pokemon)
    {
        PokemonSO tmp = ScriptableObject.CreateInstance<PokemonSO>();
        tmp.setAttributes(pokemon);
        string path = Path.Join("Assets/Resources/SO", pokemon.Name + ".asset");
        UnityEditor.AssetDatabase.CreateAsset(tmp, path);
        return tmp;
    }

    public static void OverridePokemonSO(PokemonSO pokemonSo) {
        PokemonSO tmp = pokemonSo;
        string path = Path.Join("Assets/Resources/SO", pokemonSo.GetName() + "wE" + ".asset");
        UnityEditor.AssetDatabase.CreateAsset(tmp, path);
    }

}
