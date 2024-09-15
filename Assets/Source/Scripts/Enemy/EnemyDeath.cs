using System;
using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyAnimator))]
public class EnemyDeath : MonoBehaviour
{
    private readonly int _timeDestory = 3;

    [HideInInspector][SerializeField] private EnemyHealth _enemyHealth;
    [HideInInspector][SerializeField] private EnemyAnimator _enemyAnimator;
    [HideInInspector][SerializeField] private AgentMoveToPlayer _agentMoveToPlayer;
    [HideInInspector][SerializeField] private RotateToPlayer _rotateToPlayer;

    [SerializeField] private GameObject _deathFX;

    public event Action HappendChanged;

    private void OnValidate()
    {
        _enemyHealth ??= GetComponent<EnemyHealth>();
        _enemyAnimator ??= GetComponent<EnemyAnimator>();
        _agentMoveToPlayer ??= GetComponent<AgentMoveToPlayer>();
        _rotateToPlayer ??= GetComponent<RotateToPlayer>();
    }

    private void OnEnable() => _enemyHealth.HealthChanged += OnHealthChanged;

    private void OnDisable() => _enemyHealth.HealthChanged -= OnHealthChanged;

    private void OnHealthChanged()
    {
        if (_enemyHealth.Current <= 0)
            DieEnemy();
    }

    private void DieEnemy()
    {
        _enemyAnimator.PlayDeath();

        DisableComponent();

        SpawmDeathFX();

        Destroy(gameObject, _timeDestory);

        HappendChanged?.Invoke();
    }

    private void SpawmDeathFX()
    {
        Instantiate(_deathFX, transform.position, Quaternion.identity);
    }

    private void DisableComponent()
    {
        _agentMoveToPlayer.enabled = false;
        _rotateToPlayer.enabled = false;
    }
}