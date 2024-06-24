using UnityEngine;

public class ActorUI : MonoBehaviour
{ 
    [SerializeField] private HealthBar _healthBar;

    private PlayerHealth _playerHealth;

    public void Initialize(PlayerHealth playerHealth )
    {
        _playerHealth = playerHealth;

        _playerHealth.HealthChanged += UpdateUI;
    }

    private void OnDisable()
    {
        _playerHealth.HealthChanged -= UpdateUI;
    }

    private void UpdateUI()
    {
        _healthBar.UpdateValue(_playerHealth.CurrentHealth, _playerHealth.MaxHealth);
    }
}