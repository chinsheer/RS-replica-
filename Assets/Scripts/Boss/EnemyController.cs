using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private AttackRunner _runner;
    [SerializeField] private List<AttackData> _attacks;
    [SerializeField] private Transform _target;

    private bool _busy;
    private Attack[] _attacksInstances;

    void Awake()
    {
        _attacksInstances = new Attack[_attacks.Count];
        for (int i = 0; i < _attacks.Count; i++)
        {
            _attacksInstances[i] = new Attack(_attacks[i]);
        }
    }

    void Update()
    {
        if (_busy) return;

        var attack = ChooseNextAttack();

        if (attack != null)
            StartCoroutine(ExecuteAttack(attack));
    }

    IEnumerator ExecuteAttack(Attack attack)
    {
        _busy = true;
        yield return _runner.Run(attack, _target);
        _busy = false;
    }

    Attack ChooseNextAttack()
    {
        // Simple random choice for demonstration purposes
        int index = Random.Range(0, _attacksInstances.Length);
        return _attacksInstances[index];
    }
}
