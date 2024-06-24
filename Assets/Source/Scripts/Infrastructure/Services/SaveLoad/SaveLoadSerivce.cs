using UnityEngine;

public class SaveLoadSerivce : ISaveLoadSerivce
{
    private const string ProgressKey = "ProgressKey";
    private readonly IPersistentProggressService _progressService;
    private readonly IGameFactory _gameFactory;

    public SaveLoadSerivce(IPersistentProggressService progress, IGameFactory gameFactory)
    {
        _progressService = progress;
        _gameFactory = gameFactory;
    }

    public void SaveProggress()
    {
        foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
            progressWriter.UpdateProgress(_progressService.PlayerProgress);

        PlayerPrefs.SetString(ProgressKey, _progressService.PlayerProgress.ToJson());
    }

    public PlayerProgress LoadProggress()
    {
        return PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
    }
}