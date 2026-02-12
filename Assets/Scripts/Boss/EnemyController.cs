using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyController : MonoBehaviour, IBossContext
{
    private PatternRunner _runner;
    [SerializeField] private Transform _target;
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private EnemyMinionManager _minionManager;
    [SerializeField] private PatternData _startPattern;
    [SerializeField] private PatternData[] _allPatterns;

    private bool _busy;

    // IBossContext implementation
    public Transform Boss => transform;
    public Transform Player => _target;
    public float currentHealth => _health.CurrentHP;
    public float maxHealth => _health.MaxHP;

    void Awake()
    {
        _runner = GetComponent<PatternRunner>();
        if (_runner == null)
        {
            Debug.LogError("PatternRunner component missing on EnemyController GameObject.");
        }
        _runner.Initialize(_startPattern, _allPatterns);
    }
}
