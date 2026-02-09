using UnityEngine;
using System.Collections;

public class AttackRunner : MonoBehaviour
{
    [SerializeField] private Transform _target;

    public IEnumerator Run(Attack attack)
    {
        attack.TriggerCooldown();

        yield return attack.AttackDataInstance.Indicator(gameObject, _target);

        yield return attack.AttackDataInstance.Execute(gameObject, _target);

        yield return new WaitForSeconds(attack.AttackDataInstance.RecoverTime);
        
    }
}
