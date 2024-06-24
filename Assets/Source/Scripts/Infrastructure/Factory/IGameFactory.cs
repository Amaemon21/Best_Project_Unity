using System;
using System.Collections.Generic;
using UnityEngine;

public interface IGameFactory : IService
{
    List<ISavedProgressReader> ProgressReaders { get; }

    List<ISavedProgress> ProgressWriters { get; }
    
    public GameObject PlayerGameObject { get; }

    public event Action PlayerCreated;

    public GameObject CreatePlayer(InitialPoint initialPoint);

    public GameObject CreateUI();
    
    public void CleanUp();
}