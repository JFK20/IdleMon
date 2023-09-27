using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    protected int ID;
    protected string Name;
    private float attackDuration;
    protected int Level;
    protected Move[] Moves;
    private Dictionary<Move, int> moveSet;
    
    private float lastMoveTime;
    private int initialisedMoves;
    
    public delegate void AttackEvent(Move move);

    public static event AttackEvent OnAttack;

    public Pokemon(int id, string name, float attackDuration,int level, Move[] moves) {
        this.ID = id;
        this.Name = name;
        this.attackDuration = attackDuration;
        this.Level = level;
        this.Moves = moves;
        this.initialisedMoves = 0;
        foreach (Move move in Moves) {
            if (move != null) {
                initialisedMoves++;
            }
        }
    }
    
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
}
