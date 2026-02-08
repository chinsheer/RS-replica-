using UnityEngine;
using System.Collections;

public class BossAttackRunner : MonoBehaviour
{
    [SerializeField] private Transform _target;

    public IEnumerator Run(BossAttack attack)
    {
        attack.TriggerCooldown();

        EffectHandle indicatorHandle = attack.AttackData.Indicator(gameObject, _target);
        yield return new WaitForSeconds(attack.AttackData.ChargeTime);
        indicatorHandle.Dispose();

        EffectHandle executeHandle = attack.AttackData.Execute(gameObject, _target);
        yield return new WaitForSeconds(attack.AttackData.ActiveTime);
        executeHandle.Dispose();

        yield return new WaitForSeconds(attack.AttackData.RecoverTime);
        
    }
}
