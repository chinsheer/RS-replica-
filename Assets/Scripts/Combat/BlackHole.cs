using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float Radius = 20f;
    public float PullStrength = 100f;
    public float Duration = 5f;
    void FixedUpdate()
    {
        Duration -= Time.fixedDeltaTime;
        if (Duration <= 0f)
        {
            Destroy(gameObject);
            return;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Radius);

        foreach (Collider2D collider in colliders)
        {
            Vector2 direction = (transform.position - collider.transform.position).normalized;
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(direction * PullStrength / Mathf.Max(direction.magnitude, 0.1f), ForceMode2D.Force);
            }
        }
    }
}
