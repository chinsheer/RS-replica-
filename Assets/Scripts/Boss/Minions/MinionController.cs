using System.Collections;
using UnityEngine;

public class MinionController : MonoBehaviour, IBossContext
{
    private MinionData _data;
    private Transform _target;

    // IBossContext implementation
    public Transform Boss => gameObject.transform;
    public Transform Player => _target;
    public float currentHealth => gameObject.GetComponent<EnemyHealth>().CurrentHP;
    public float maxHealth => gameObject.GetComponent<EnemyHealth>().MaxHP; 
    public MonoBehaviour Runner => gameObject.GetComponent<PatternRunner>();

    public void Initialize(MinionData data, Transform target, Vector2 position)
    {
        _data = data;
        _target = target;
        gameObject.GetComponent<EnemyHealth>().MaxHP = _data.MaxHp;
        gameObject.AddComponent<MoveToPoint2D>().SetGoal(position);
        gameObject.AddComponent<PatternRunner>().Initialize(_data.StartPattern, _data.AllPatterns);
    }
}
