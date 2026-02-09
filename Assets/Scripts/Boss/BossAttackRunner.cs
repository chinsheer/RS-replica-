using UnityEngine;
using System.Collections;

public class BossAttackRunner : MonoBehaviour
{
    [SerializeField] private Transform _target;

    public IEnumerator Run(BossAttack attack)
    {
        attack.TriggerCooldown();

        yield return attack.AttackData.Indicator(gameObject, _target);

        yield return attack.AttackData.Execute(gameObject, _target);

        yield return new WaitForSeconds(attack.AttackData.RecoverTime);
        
    }
}
