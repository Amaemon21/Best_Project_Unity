using System;

public interface IHealth
{
    public float Current { get; }
    
    public float Max { get; }

    public event Action HealthChanged;

    public void TakeDamage(float damage);
}