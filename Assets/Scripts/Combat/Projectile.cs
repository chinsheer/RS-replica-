using UnityEngine;

public class Projectile : MonoBehaviour
{
    public DamageAttribute Damage;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<Hurtbox>().DamageOwner.TakeDamage(Damage);
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
