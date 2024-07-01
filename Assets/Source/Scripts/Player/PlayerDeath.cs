using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerMove))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerAttack))]
public class PlayerDeath : MonoBehaviour
{
    [HideInInspector] [SerializeField] private PlayerAnimator _playerAnimator;
    [HideInInspector] [SerializeField] private PlayerMove _playerMove;
    [HideInInspector] [SerializeField] private PlayerHealth _playerHealth;
    [HideInInspector] [SerializeField] private PlayerAttack _playerAttack;

    [SerializeField] private GameObject _DeathFX;
    private bool _isDead;

    private void OnValidate()
    {
        _playerAnimator ??= GetComponent<PlayerAnimator>();
        _playerMove ??= GetComponent<PlayerMove>();
        _playerHealth ??= GetComponent<PlayerHealth>();
        _playerAttack ??= GetComponent<PlayerAttack>();
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
        if (!_isDead && _playerHealth.Current <= 0)
            Die();
    }

    private void Die()
    {
        _isDead = true;

        _playerMove.enabled = false;
        _playerAttack.enabled = false;

        _playerAnimator.PlayDeath();

        Instantiate(_DeathFX, transform.position, Quaternion.identity);
    }
}
