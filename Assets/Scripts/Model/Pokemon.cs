using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Pokemon
{
    private MovesManager movesManager;
    
    public string Name;
    public string Types;
    public string BaseStats;
    public string GenderRatio;
    public string Ability;
    public string HiddenAbilities;
    public string MoveSet;
    public string Habitat;
    public string Evolutions;
    public string Description;
    
    public Pokemon(string name, string types, string baseStats, string genderRatio, string ability, string hiddenAbilities, string moveSet, string habitat, string evolutions, string description) {
        Name = name;
        Types = types;
        BaseStats = baseStats;
        GenderRatio = genderRatio;
        Ability = ability;
        HiddenAbilities = hiddenAbilities;
        MoveSet = moveSet;
        Habitat = habitat;
        Evolutions = evolutions;
        Description = description;
        
    }

    private Dictionary<Stats, int> InitBaseStats(string baseStatsString) {
        string[] TMPbaseStats = baseStatsString.Split(",");
        Dictionary<Stats, int> TMPbaseStatsDict = new Dictionary<Stats, int>(6) {
            { Stats.HP, int.Parse(TMPbaseStats[0]) },
            { Stats.Attack, int.Parse(TMPbaseStats[1]) },
            { Stats.Defense, int.Parse(TMPbaseStats[2]) },
            { Stats.Speed, int.Parse(TMPbaseStats[3]) },
            { Stats.SpecialAttack, int.Parse(TMPbaseStats[4]) },
            { Stats.SpecialDefense, int.Parse(TMPbaseStats[5]) }
        };
        
        return TMPbaseStatsDict;
    }

    private Dictionary<Move, int> InitMoveSet(string moveSetString) {
        movesManager = GameObject.FindObjectOfType<MovesManager>();
        Dictionary<Move,int> TMPmoveSetDict = new Dictionary<Move, int>();
        string[] TMPmoveSet = moveSetString.Split(",");
        for (int i = 0; i < TMPmoveSet.Length; i += 2) {
            if (TMPmoveSet[i] != null && TMPmoveSet[i+1] != null){
                if(!TMPmoveSetDict.ContainsKey(movesManager.GetMoveByName(TMPmoveSet[i+1])))
                { 
                    TMPmoveSetDict.Add(movesManager.GetMoveByName(TMPmoveSet[i+1]), int.Parse(TMPmoveSet[i]));
                }
            }
        }
        return TMPmoveSetDict;
    }

    private List<MoveType> InitTypes(string typesString) {
        List<MoveType> tmpList = new List<MoveType>();
        
        string[] TMPtype = typesString.Split(",");
        foreach (var type in TMPtype) {
            MoveType tmp = EnumSwitches.EnumSwitch<MoveType>(type);
            tmpList.Add(tmp);
        }

        return tmpList;
    }
    
    private List<MoveType> types = new List<MoveType>();
    private Dictionary<Stats, int> baseStats;
    private GenderRatio genderRatio;
    private Dictionary<Move, int> moveSet;
    private Habitat habitat;
    private Dictionary<Pokemon, int> evolutions;

    public void SetData() {
        this.types = InitTypes(Types);
        
        this.baseStats = InitBaseStats(BaseStats);
        
         genderRatio =EnumSwitches.EnumSwitch<GenderRatio>(GenderRatio);
        
        this.moveSet = InitMoveSet(MoveSet);
        
        habitat = EnumSwitches.EnumSwitch<Habitat>(Habitat);
        
        this.initialisedMoves = 0;
        if (Moves != null)
        {
            foreach (Move move in Moves) {
                if (move != null) {
                    initialisedMoves++;
                }
            }
        }
    }
    
    
    
    private Move[] Moves;
    private float attackDuration;
    protected int Level;
    
    
    private float lastMoveTime;
    private int initialisedMoves;
    
    public delegate void AttackEvent(Move move);

    public static event AttackEvent OnAttack;
    
    public void OnValidate() {
        if (Moves.Length > 4 && Moves.Length < 1) {
            throw new ArgumentOutOfRangeException("moves", "Pokemon can only have 1-4 moves");
        }
    }

    private void Start() {
        lastMoveTime = 0;
    }

    private void Attack() {
        OnAttack?.Invoke(Moves[MoveChooser()]);
    }

    private void Update() {
        lastMoveTime += Time.deltaTime;
        while (lastMoveTime >= attackDuration) {
            Attack();
            lastMoveTime -= attackDuration;
        }
    }

    private int MoveChooser() {
        float range = UnityEngine.Random.Range(0, initialisedMoves);
        return Mathf.RoundToInt(range);
    }

    public override string ToString() {
        return $"Name :{Name}, Types: {Types}, BaseStats: {BaseStats}, GenderRatio: {GenderRatio},  MoveSet: {MoveSet}, Habitat: {Habitat}, Evolutions: {Evolutions}";
    }
}
