using UnityEngine;

public class SaveLoadService : ISaveLoadSerivce
{
    private const string DataKey = "ProgressKey";
    private readonly IPersistentProggressService _progressService;
    private readonly IGameFactory _gameFactory;

    public SaveLoadService(IPersistentProggressService progress, IGameFactory gameFactory)
    {
        _progressService = progress;
        _gameFactory = gameFactory;
    }

    public void SaveProgress()
    {
        foreach (var progressWriter in _gameFactory.ProgressWriters)
            progressWriter.UpdateProgress(_progressService.PlayerProgress);

        PlayerPrefs.SetString(DataKey, _progressService.PlayerProgress.ToJson());
    }

    public PlayerProgress LoadProgress()
    {
        return PlayerPrefs.GetString(DataKey)?.ToDeserialized<PlayerProgress>();
    }
}