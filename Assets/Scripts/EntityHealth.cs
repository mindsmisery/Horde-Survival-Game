using System;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _healthRegen;

    public Action OnDeath;
    public Action<float, float> OnHealthChanged;

    void Awake()
    {
        ResetHealth();
    }

    void Start()
    {
        InvokeRepeating(nameof(HandleHealthRegen), 1f, 1f);
    }

    public void LoseHealth(float healthLost)
    {
        _currentHealth -= healthLost;
        OnHealthChanged?.Invoke(Mathf.Clamp(_currentHealth, 0, _maxHealth), _maxHealth);
        if (_currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        OnDeath?.Invoke();
    }

    void HandleHealthRegen()
    {
        _currentHealth = Mathf.Clamp(_currentHealth + _maxHealth * _healthRegen, 0, _maxHealth);
        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
    }
}
