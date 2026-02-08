using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int MaxHP = 7;
    public int CurrentHP{ get { return _currentHP; } }

    public event Action<int> OnHealthChanged;
    
    private int _currentHP;

    void Awake()
    {
        _currentHP = MaxHP;
        OnHealthChanged?.Invoke(_currentHP);
    }

    public void TakeDamage(DamageAttribute damage)
    {
        if(damage.DamageAmount <= 0) return;
        _currentHP -= 1;
        _currentHP = Mathf.Clamp(_currentHP, 0, MaxHP);
        OnHealthChanged?.Invoke(_currentHP);
    }
}
