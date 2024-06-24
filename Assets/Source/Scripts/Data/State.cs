 using System;

[Serializable]
public class State
{
    public float CurrentHealth;
    public float MaxHealth;

    public void ResetHealth()
    {
        CurrentHealth = MaxHealth;
    }
}