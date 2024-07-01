using System;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
public class PlayerHealth : MonoBehaviour, ISavedProgress, IHealth
{
    [HideInInspector][SerializeField] private PlayerAnimator _playerAnimator;

    private HealthStats _state;
    private ISaveLoadSerivce _saveLoadServer;

    public event Action HealthChanged;

    public float Current
    {
        get => _state.CurrentHealth;
        private set
        {
            _state.CurrentHealth = value;
            HealthChanged?.Invoke();
        }
    }

    public float Max
    {
        get => _state.MaxHealth;
        private set => _state.MaxHealth = value;
    }

    private void OnValidate()
    {
        _playerAnimator ??= GetComponent<PlayerAnimator>();
    }

    private void Awake()
    {
        _saveLoadServer = AllServices.Container.Single<ISaveLoadSerivce>();
    }

    public void LoadProgress(PlayerProgress proggress)
    {
        _state = proggress.HealthStats;
        HealthChanged?.Invoke();
    }

    public void UpdateProgress(PlayerProgress proggress)
    {
        proggress.HealthStats.CurrentHealth = Current;
        proggress.HealthStats.MaxHealth = Max;
    }

    public void TakeDamage(float damage)
    {
        HealthChanged?.Invoke();

        _saveLoadServer.SaveProggress();

        Current -= damage;

        _playerAnimator.PlayHit();
    }
}