using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveToPoint2D : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float arriveDistance = 0.1f;

    private Rigidbody2D _rb;
    private Vector2 _goal;
    private bool _hasGoal;

    void Awake() => _rb = GetComponent<Rigidbody2D>();

    public void SetGoal(Vector2 goal)
    {
        _goal = goal;
        _hasGoal = true;
    }

    void FixedUpdate()
    {
        if (!_hasGoal) return;

        Vector2 pos = _rb.position;
        Vector2 toGoal = _goal - pos;

        if (toGoal.sqrMagnitude <= arriveDistance * arriveDistance)
        {
            _rb.linearVelocity = Vector2.zero;
            _rb.MovePosition(_goal);
            _hasGoal = false;
            return;
        }

        _rb.linearVelocity = toGoal.normalized * speed;
    }
}

