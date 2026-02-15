using System.Collections.Generic;
using UnityEngine;

public class InvincibleZone : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Hurtbox>(out Hurtbox hurtbox))
        {
            if(hurtbox.StatusOwner != null)
            {
                StatusEffectManager statusEffect = hurtbox.StatusOwner.GetStatusEffect();

                statusEffect.GrantZoneInvincibility();
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Hurtbox>(out Hurtbox hurtbox))
        {
            if(hurtbox.StatusOwner != null)
            {
                StatusEffectManager statusEffect = hurtbox.StatusOwner.GetStatusEffect();
                statusEffect.RevokeZoneInvincibility();
            }
        }
    }
}
