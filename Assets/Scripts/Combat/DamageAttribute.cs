using UnityEngine;

public struct DamageAttribute
{
    public float DamageAmount;
    public GameObject DamageSource;

    public DamageAttribute(float damageAmount, GameObject damageSource)
    {
        DamageAmount = damageAmount;
        DamageSource = damageSource;
    }
}
