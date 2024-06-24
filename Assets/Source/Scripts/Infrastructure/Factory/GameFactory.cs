using System;
using System.Collections.Generic;
using UnityEngine;

public class GameFactory : IGameFactory
{
    private readonly IAssetProvider _assets;

    public event Action PlayerCreated;

    public List<ISavedProgressReader> ProgressReaders { get; } = new();
    public List<ISavedProgress> ProgressWriters { get; } = new();

    public GameObject PlayerGameObject { get; set; }

    public GameFactory(IAssetProvider assets)
    {
        _assets = assets;
    }

    public GameObject CreatePlayer(InitialPoint initialPoint)
    {
        PlayerGameObject = InstantiateRegistred(AssetsPath.PlayerPath, initialPoint.transform.position);
        PlayerCreated?.Invoke();
        return PlayerGameObject;
    }

    public GameObject CreateUI()
    {
        return InstantiateRegistred(AssetsPath.UIPath);
    }

    public void CleanUp()
    {
        ProgressReaders.Clear();
        ProgressWriters.Clear();
    }

    private GameObject InstantiateRegistred(string prefabPath, Vector3 position)
    {
        GameObject gameObject = _assets.Instantiate(prefabPath, position);
        RegisterProgressWatchers(gameObject);
        return gameObject;
    }

    private GameObject InstantiateRegistred(string prefabPath)
    {
        GameObject gameObject = _assets.Instantiate(prefabPath);
        RegisterProgressWatchers(gameObject);
        return gameObject;
    }

    private void RegisterProgressWatchers(GameObject gameObject)
    {
        foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            Register(progressReader);
    }

    private void Register(ISavedProgressReader progressReader)
    {
        if (progressReader is ISavedProgress progressWriter)
            ProgressWriters.Add(progressWriter);

        ProgressReaders.Add(progressReader);
    }
}