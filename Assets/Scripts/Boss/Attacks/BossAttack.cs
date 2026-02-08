using UnityEngine;

public class BossAttack
{
    public BossAttackData AttackData { get; private set; }
    float _nextReadyTime = 0f;

    public BossAttack(BossAttackData attackData)
    {
        AttackData = attackData;
    }

    public bool IsReady()
    {
        return Time.time >= _nextReadyTime;
    }
    public void TriggerCooldown()
    {
        _nextReadyTime = Time.time + AttackData.CooldownTime;
    }
}
