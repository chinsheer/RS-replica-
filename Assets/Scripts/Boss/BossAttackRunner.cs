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
        indicatorHandle?.Dispose();

        yield return ActivePhase(attack.AttackData, gameObject, _target);

        yield return new WaitForSeconds(attack.AttackData.RecoverTime);
        
    }

    IEnumerator ActivePhase(BossAttackData attackData, GameObject boss, Transform target)
    {
        EffectHandle _currentEffect = attackData.Execute(boss, target);

        float elapsed = 0f;

        while (elapsed < attackData.ActiveTime)
        {
            float dt = Time.deltaTime;

            _currentEffect?.Tick(dt);

            elapsed += dt;
            yield return null;
        }

        _currentEffect?.Dispose();
        _currentEffect = null;
    }
}
