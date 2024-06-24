using System;

[Serializable]
public class PlayerProgress
{
    public WorldData WorldData;
    public State PlayerState;

    public PlayerProgress(string initialLevel)
    {
        WorldData = new WorldData(initialLevel);
        PlayerState = new State();
    }
}
