using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 5f;

    private Rigidbody2D _rb;
    private InputAction _moveInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _moveInput = InputSystem.actions.FindAction("Move");
    }

    void FixedUpdate()
    {
        Vector2 moveInput = _moveInput.ReadValue<Vector2>();
        _rb.linearVelocity = moveInput * MoveSpeed;
    }
}
