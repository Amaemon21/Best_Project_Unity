using UnityEngine;

public class LoadLevelState : IPayloadedState<string>
{
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingScreen _loadingScreen;
    private readonly IGameFactory _gameFactory;
    private readonly IPersistentProggressService _persistentProggressService;

    public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen, IGameFactory gameFactory, IPersistentProggressService persistentProggressService)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        _loadingScreen = loadingScreen;
        _gameFactory = gameFactory;
        _persistentProggressService = persistentProggressService;
    }

    public void Enter(string nameScene)
    {
        _loadingScreen.Show();
        _gameFactory.CleanUp();
        _sceneLoader.Load(nameScene, OnLoaded);
    }

    public void Exit()
    {
        _loadingScreen.Hide();
    }

    private void OnLoaded()
    {
        InitGameWorld();
        InformProgressReaders();

        _stateMachine.Enter<GameLoopState>();
    }

    private void InformProgressReaders()
    {
        foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
        {
            progressReader.LoadProgress(_persistentProggressService.PlayerProgress);
        }
    }

    private void InitGameWorld()
    {
        GameObject player = _gameFactory.CreatePlayer(Object.FindFirstObjectByType<InitialPoint>());

        GameObject HUD = _gameFactory.CreateUI();

        HUD.GetComponent<ActorUI>().Construct(player.GetComponent<IHealth>());

        CameraFollow(player);
    }

    private void CameraFollow(GameObject player)
    {
        Camera.main.GetComponent<CameraFollow>().Follow(player.transform);
    }
}