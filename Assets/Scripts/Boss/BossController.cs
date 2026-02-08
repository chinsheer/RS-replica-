using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BossController : MonoBehaviour
{
    [SerializeField] private BossAttackRunner _runner;
    [SerializeField] private List<BossAttackData> _attacks;

    private bool _busy;
    private BossAttack[] _bossAttacks;

    void Awake()
    {
        _bossAttacks = new BossAttack[_attacks.Count];
        for (int i = 0; i < _attacks.Count; i++)
        {
            _bossAttacks[i] = new BossAttack(_attacks[i]);
        }
    }

    void Update()
    {
        if (_busy) return;

        var attack = ChooseNextAttack();

        if (attack != null)
            StartCoroutine(ExecuteAttack(attack));
    }

    IEnumerator ExecuteAttack(BossAttack attack)
    {
        _busy = true;
        yield return _runner.Run(attack);
        _busy = false;
    }

    BossAttack ChooseNextAttack()
    {
        // Simple random choice for demonstration purposes
        int index = Random.Range(0, _bossAttacks.Length);
        return _bossAttacks[index];
    }
}
