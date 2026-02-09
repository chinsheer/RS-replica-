using System.Collections;
using UnityEngine;

public class MinionController : MonoBehaviour
{
    private MinionData _data;
    private Transform _target;
    private Attack attack;

    public void Initialize(MinionData data, Transform target, Vector2 position)
    {
        _data = data;
        _target = target;
        attack = new Attack(_data.Attack);
        gameObject.GetComponent<EnemyHealth>().MaxHP = _data.MaxHp;
        gameObject.AddComponent<AttackRunner>();
        gameObject.AddComponent<MoveToPoint2D>().SetGoal(position);
        StartCoroutine(ExecuteAttack());
    }

    public IEnumerator ExecuteAttack()
    {
        while (true)
        {
            yield return gameObject.GetComponent<AttackRunner>().Run(attack, _target);
        }
    }
}
