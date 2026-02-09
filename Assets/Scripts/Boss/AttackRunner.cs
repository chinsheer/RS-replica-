using UnityEngine;
using System.Collections;

public class AttackRunner : MonoBehaviour
{

    public IEnumerator Run(Attack attack, Transform target)
    {
        attack.TriggerCooldown();

        yield return attack.AttackDataInstance.Indicator(gameObject, target);

        yield return attack.AttackDataInstance.Execute(gameObject, target);

        yield return new WaitForSeconds(attack.AttackDataInstance.RecoverTime);
        
    }
}
