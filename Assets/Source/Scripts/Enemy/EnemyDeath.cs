using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyAnimator))]
public class EnemyDeath : MonoBehaviour
{
    private readonly int _timeDestory = 3;

    [HideInInspector][SerializeField] private EnemyHealth _enemyHealth;
    [HideInInspector][SerializeField] private EnemyAnimator _enemyAnimator;

    [SerializeField] private GameObject _deathFX;

    public event Action Happend;

    private void OnValidate()
    {
        _enemyHealth ??= GetComponent<EnemyHealth>();
        _enemyAnimator ??= GetComponent<EnemyAnimator>();
    }

    private void OnEnable() => _enemyHealth.HealthChanged += OnHealthChanged;

    private void OnDisable() => _enemyHealth.HealthChanged -= OnHealthChanged;

    private void OnHealthChanged()
    {
        if (_enemyHealth.Current <= 0)
            Die();
    }

    private void Die()
    {
        _enemyHealth.HealthChanged -= OnHealthChanged;

        _enemyAnimator.PlayDeath();

        SpawmDeathFX();

        StartCoroutine(DestroyTimer());

        Happend?.Invoke();
    }


    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(_timeDestory);
        Destroy(gameObject);
    }

    private void SpawmDeathFX()
    {
        Instantiate(_deathFX, transform.position, Quaternion.identity);
    }
}