using System;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
public class EnemyHealth : MonoBehaviour, IHealth
{
    [HideInInspector] [SerializeField] private EnemyAnimator _enemyAnimator;

    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maxHealth;

    public float Current
    {
        get => _currentHealth;
        set => _currentHealth = value;
    }

    public float Max
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }

    public event Action HealthChanged;

    private void OnValidate()
    {
        _enemyAnimator ??= GetComponent<EnemyAnimator>();
    }

    public void TakeDamage(float damage)
    {
        Current -= damage;

        _enemyAnimator.PlayHit();

        HealthChanged?.Invoke();
    }
}