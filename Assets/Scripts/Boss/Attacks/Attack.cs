using UnityEngine;

public class Attack
{
    public AttackData AttackDataInstance { get; private set; }
    float _nextReadyTime = 0f;

    public Attack(AttackData attackData)
    {
        AttackDataInstance = attackData;
    }

    public bool IsReady()
    {
        return Time.time >= _nextReadyTime;
    }
    public void TriggerCooldown()
    {
        _nextReadyTime = Time.time + AttackDataInstance.CooldownTime;
    }
}
