using UnityEngine;

[CreateAssetMenu(fileName = "BossData", menuName = "Scriptable Objects/BossData")]
public class BossData : ScriptableObject
{
    [Header("Identity")]
    public string bossId;
    public GameObject bossPrefab;
    public Vector3 spawnPosition;

    [Header("Health")]
    public int maxHp = 300;

    [Header("Minions")]
    public int maxMinions = 3;

    [Header("Patterns (Markov Graph)")]
    public PatternData startPattern;
    public PatternData[] allPatterns;

    [Header("Optional: Phase thresholds (HP%)")]
    [Range(0f, 1f)] public float phase2At = 0.66f;
    [Range(0f, 1f)] public float phase3At = 0.33f;

    [Header("Optional: global weight tuning")]
    public int selfLoopPenalty = 0; 
}
