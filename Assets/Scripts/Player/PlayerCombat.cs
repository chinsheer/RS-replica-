using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private PlayerSkills _playerSkills;
    private InputAction _skill1;
    private InputAction _skill2;
    private InputAction _skill3;
    private InputAction _skill4;

    void Awake()
    {
        _playerSkills = GetComponent<PlayerSkills>();
        _skill1 = InputSystem.actions.FindAction("Attack1");
        _skill2 = InputSystem.actions.FindAction("Attack2");
        _skill3 = InputSystem.actions.FindAction("Attack3");
        _skill4 = InputSystem.actions.FindAction("Attack4");

        _skill1.performed += ctx => _playerSkills.TryCast(0);
        _skill2.performed += ctx => _playerSkills.TryCast(1);
        _skill3.performed += ctx => _playerSkills.TryCast(2);
        _skill4.performed += ctx => _playerSkills.TryCast(3);
    }

    
}
