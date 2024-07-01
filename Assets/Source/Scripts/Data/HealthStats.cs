 using System;

[Serializable]
public class HealthStats
{
    public float CurrentHealth;
    public float MaxHealth;

    public void ResetHealth()
    {
        CurrentHealth = MaxHealth;
    }
}