using UnityEngine;

[CreateAssetMenu(fileName = "BossData", menuName = "Scriptable Objects/BossData")]
public class BossData : ScriptableObject
{
    [Header("Identity")]
    public string BossId;
    public GameObject BossPrefab;
    public Vector3 SpawnPosition;

    [Header("Health")]
    public int MaxHP = 300;

    [Header("Minions")]
    public int MaxMinions = 3;

    [Header("Patterns (Markov Graph)")]
    public BossPhase[] Phases;

    [Header("Optional: global weight tuning")]
    public int SelfLoopPenalty = 0; 
}
