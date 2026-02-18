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

    public float ChargeTime { get { return _chargeTime; } set { _chargeTime = value; } }
    public float ActiveTime { get { return _activeTime; } set { _activeTime = value; } }
    public float RecoverTime { get { return _recoverTime; } set { _recoverTime = value; } }

    public abstract IEnumerator Indicator(IBossContext ctx);
    public abstract IEnumerator Execute(IBossContext ctx);
    public abstract IEnumerator Recover(IBossContext ctx);
}
