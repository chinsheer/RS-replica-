using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyController : MonoBehaviour, IBossContext
{    
    private Transform _target;
    private PatternRunner _runner;
    private EnemyHealth _health;
    private EnemyMinionManager _minionManager;

    private bool _busy;

    // IBossContext implementation
    public Transform Boss => transform;
    public Transform Player => _target;
    public float currentHealth => _health.CurrentHP;
    public float maxHealth => _health.MaxHP;

    public void Initialize(BossData data, GameObject player)
    {
        _runner = GetComponent<PatternRunner>();
        _health = GetComponent<EnemyHealth>();
        _minionManager = GetComponent<EnemyMinionManager>();
        _target = player.transform;

        _runner.Initialize(data.startPattern, data.allPatterns);
        _health.Initialize(data.maxHp);
        _minionManager.Initialize(data.maxMinions);
    }
}
