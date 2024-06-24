public interface ISavedProgressReader
{
    public void LoadProgress(PlayerProgress proggress);
}

public interface ISavedProgress : ISavedProgressReader
{
    public void UpdateProgress(PlayerProgress proggress);
}
