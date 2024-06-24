using System;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
public class PlayerHealth : MonoBehaviour, ISavedProgress
{
    [HideInInspector] [SerializeField] private PlayerAnimator _playerAnimator;

    private State _state;
    private ISaveLoadSerivce _saveLoadServer;

    public event Action HealthChanged; 

    public float CurrentHealth
    {
        get => _state.CurrentHealth;
        private set => _state.CurrentHealth = value;
    }

    public float MaxHealth
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
        _state = proggress.PlayerState;
        HealthChanged?.Invoke();
    }

    public void UpdateProgress(PlayerProgress proggress)
    {
        proggress.PlayerState.CurrentHealth = CurrentHealth;
        proggress.PlayerState.MaxHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0) 
            return;

        if (CurrentHealth <= 0)
            return;

        CurrentHealth -= damage;

        _saveLoadServer.SaveProggress();
        HealthChanged?.Invoke();

        _playerAnimator.PlayHit();
    }
}