using System;
using System.Collections.Generic;
using UnityEngine;

public class BossPhaseController : MonoBehaviour
{
    private BossPhase[] _phases;
    private PatternRunner _runner;
    private int _currentPhaseIndex = 0;

    public void Initialize(BossPhase[] phases)
    {
        _phases = phases;
        _runner = gameObject.AddComponent<PatternRunner>();
        StartPhase();
        EnemyHealth health = GetComponent<EnemyHealth>();
        health.OnHealthChanged += CheckPhaseTransition;
    }

    public void StartPhase()
    {
        if (_phases == null || _phases.Length == 0) return;
        BossPhase currentPhase = _phases[_currentPhaseIndex];
        _runner.Initialize(currentPhase.StartPattern, currentPhase.AllPatterns);
    }

    public void NextPhase()
    {
        if (_runner != null)
        {
            _runner.Stop();
        }
        _currentPhaseIndex++;
        if (_currentPhaseIndex < _phases.Length)
        {
            StartPhase();
        }
    }

    private void CheckPhaseTransition(float currentHP, float maxHP)
    {
        float hpPercent = currentHP / maxHP;
        if (hpPercent <= _phases[_currentPhaseIndex].HPThreshold)
        {
            NextPhase();
        }
    }
}
