using System;
using UnityEngine;

public class BouncingBullet : MonoBehaviour
{
    public Action OnBounce;
    public Action OnDestroy;
    public DamageAttribute Damage;

    void Start()
    {
        OnBounce += ChangeDirection;
    }

    public void Kill()
    {
        OnDestroy?.Invoke();
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            OnBounce?.Invoke();
        }
    }

    private void ChangeDirection()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        spriteRenderer.transform.right = rb.linearVelocity.normalized;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<Hurtbox>().DamageOwner.TakeDamage(Damage);
        Kill();
    }
}
