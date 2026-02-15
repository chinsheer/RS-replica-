using UnityEngine;

public class Laser : MonoBehaviour
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
