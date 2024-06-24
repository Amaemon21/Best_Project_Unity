using System;

[Serializable]
public class WorldData
{
    public PositionOnLevel PositionOnLevel;
    private string initialLevel;

    public WorldData(string initialLevel)
    {
        PositionOnLevel = new PositionOnLevel(initialLevel);
    }
}