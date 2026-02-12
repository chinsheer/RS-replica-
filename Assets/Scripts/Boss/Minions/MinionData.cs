using UnityEngine;

[CreateAssetMenu(fileName = "MinionData", menuName = "Scriptable Objects/MinionData")]
public class MinionData : ScriptableObject
{
    [Header("Prefab")]
    [SerializeField] private GameObject _prefab;

    [Header("Stats")]
    [SerializeField] private int _maxHp = 20;

    [Header("Attack")]
    [SerializeField] private PatternData startPattern;
    [SerializeField] private PatternData[] allPatterns;

    public GameObject Prefab => _prefab;
    public int MaxHp => _maxHp;
    public PatternData StartPattern => startPattern;
    public PatternData[] AllPatterns => allPatterns;
}
