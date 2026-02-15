using UnityEngine;

public class Melee : MonoBehaviour
{
    public DamageAttribute Damage = new DamageAttribute
    {
        DamageAmount = 0f,
    };

    void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<Hurtbox>().DamageOwner.TakeDamage(Damage);
    }
}
