using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyController : MonoBehaviour, IBossContext
{    
    private Transform _target;
    private PatternRunner _runner;
    private EnemyHealth _health;
    private EnemyMinionManager _minionManager;
    private BossPhaseController _phaseController;

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
        _phaseController = GetComponent<BossPhaseController>();
        _target = player.transform;

        _phaseController.Initialize(data.Phases);
        _health.Initialize(data.MaxHP);
        _minionManager.Initialize(data.MaxMinions);
    }
}
