using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public float MaxHP = 1000;
    public float CurrentHP{ get { return _currentHP; } }

    public event Action<float> OnHealthChanged;
    public event Action OnDeath;
    
    private float _currentHP;

    public void Initialize(float maxHP)
    {
        MaxHP = maxHP;
        _currentHP = MaxHP;
        OnHealthChanged?.Invoke(_currentHP);
    }

    public void TakeDamage(DamageAttribute damage)
    {
        _currentHP -= damage.DamageAmount;
        _currentHP = Mathf.Clamp(_currentHP, 0, MaxHP);
        OnHealthChanged?.Invoke(_currentHP);
        if(_currentHP <= 0)
        {
            OnDeath?.Invoke();
            Destroy(gameObject);
        }
    }
}
