using System;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerMove))]
[RequireComponent(typeof(PlayerHealth))]
public class PlayerDeath : MonoBehaviour
{
    [HideInInspector] [SerializeField] private PlayerAnimator _playerAnimator;
    [HideInInspector] [SerializeField] private PlayerMove _playerMove;
    [HideInInspector] [SerializeField] private PlayerHealth _playerHealth;

    [SerializeField] private GameObject _DeathFX;
    private bool _isDead;

    private void OnValidate()
    {
        _playerAnimator ??= GetComponent<PlayerAnimator>();
        _playerMove = GetComponent<PlayerMove>();
        _playerHealth = GetComponent<PlayerHealth>();
    }

    private void OnEnable()
    {
        _playerHealth.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _playerHealth.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged()
    {
        if (!_isDead && _playerHealth.CurrentHealth <= 0)
            Die();
    }

    private void Die()
    {
        _isDead = true;

        _playerMove.enabled = false;
        _playerAnimator.PlayDeath(); 

        Instantiate(_DeathFX, transform.position, Quaternion.identity);
    }
}
