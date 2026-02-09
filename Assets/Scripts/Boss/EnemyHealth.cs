using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public float MaxHP = 1000;
    public float CurrentHP{ get { return _currentHP; } }

    public event Action<float> OnHealthChanged;
    
    private float _currentHP;

    void Awake()
    {
        _currentHP = MaxHP;
        OnHealthChanged?.Invoke(_currentHP);
    }

    public void TakeDamage(DamageAttribute damage)
    {
        _currentHP -= damage.DamageAmount;
        _currentHP = Mathf.Clamp(_currentHP, 0, MaxHP);
        OnHealthChanged?.Invoke(_currentHP);
    }
}
