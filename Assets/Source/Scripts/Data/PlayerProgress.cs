using System;

[Serializable]
public class PlayerProgress
{
    public WorldData WorldData;
    public HealthStats HealthStats;
    public DamageStats DamageStats;

    public PlayerProgress(string initialLevel)
    {
        WorldData = new WorldData(initialLevel);
        HealthStats = new HealthStats();
        DamageStats = new DamageStats();
    }
}
