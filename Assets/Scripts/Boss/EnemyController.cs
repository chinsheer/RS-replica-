using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Timers;

public class EnemyController : MonoBehaviour, IBossContext
{    
    private Transform _target;
    private PatternRunner _runner;
    private EnemyHealth _health;
    private EnemyMinionManager _minionManager;
    private BossPhaseController _phaseController;
    private EnvironmentController _environmentController;
    private SpriteRenderer _sr;

    // IBossContext implementation
    public Transform Boss => transform;
    public Transform Player => _target;
    public float currentHealth => _health.CurrentHP;
    public float maxHealth => _health.MaxHP;
    public MonoBehaviour Runner => _runner;
    public Wall WallInstance => _environmentController.WallInstance;
    public Wall PlayerWallInstance => _environmentController.PlayerWallInstance;
    public SpriteRenderer BossSR => _sr;

    public void Initialize(BossData data, GameObject player)
    {
        _runner = GetComponent<PatternRunner>();
        _health = GetComponent<EnemyHealth>();
        _minionManager = GetComponent<EnemyMinionManager>();
        _phaseController = GetComponent<BossPhaseController>();
        _environmentController = GetComponent<EnvironmentController>();
        _sr = gameObject.transform.Find("Body").GetComponent<SpriteRenderer>();
        _target = player.transform;

        _phaseController.Initialize(data.Phases);
        _health.Initialize(data.MaxHP);
        _minionManager.Initialize(data.MaxMinions);
    }

    public IEnumerator MoveCenter(float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime <= duration)
        {
            transform.position = Vector3.Lerp(transform.position, Vector3.zero, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
}
