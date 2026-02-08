using System.Collections.Generic;
using UnityEngine;

public class InvincibleZone : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerStatusEffect>(out PlayerStatusEffect statusEffect))
        {
            statusEffect.GrantZoneInvincibility();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerStatusEffect>(out PlayerStatusEffect statusEffect))
        {
            statusEffect.RevokeZoneInvincibility();
        }
    }
}
