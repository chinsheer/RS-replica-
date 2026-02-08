using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    private Rigidbody2D _rb;

    public Transform Target;
    public float Speed = 10f;
    public float Sensitivity = 360f;
    public DamageAttribute Damage = new DamageAttribute
    {
        DamageAmount = 0f,
        DamageSource = null 
    };

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(Target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 direction = (Vector2)(Target.position - transform.position);
        direction.Normalize();

        Vector2 currentDir = _rb.linearVelocity.normalized;
        float maxTurn = Sensitivity * Time.fixedDeltaTime;
        Vector2 newDir = Vector2.Lerp(currentDir, direction, maxTurn * Mathf.Deg2Rad);
        newDir.Normalize();

        _rb.linearVelocity = newDir * Speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<Hurtbox>().Owner.TakeDamage(Damage);
        Destroy(gameObject);
    }
}
