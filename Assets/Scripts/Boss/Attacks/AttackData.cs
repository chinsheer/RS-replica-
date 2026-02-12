using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "Scriptable Objects/AttackData")]
public abstract class AttackData : ScriptableObject
{
    [SerializeField] private float _cooldownTime = 1f;
    public float CooldownTime => _cooldownTime;

    [SerializeField] private float _chargeTime = 0f;
    [SerializeField] private float _activeTime = 0f;
    [SerializeField] private float _recoverTime = 0f;

    public float ChargeTime => _chargeTime;
    public float ActiveTime => _activeTime;
    public float RecoverTime => _recoverTime;

    public abstract IEnumerator Indicator(IBossContext ctx);
    public abstract IEnumerator Execute(IBossContext ctx);
}
