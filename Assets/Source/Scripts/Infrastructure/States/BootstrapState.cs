using UnityEngine;

public class BootstrapState : IState
{
    private const string Initial = "Boot";
    private const string Gameplay = "Gameplay";

    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _services;

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        _services = services;

        RegisterServices();
    }

    public void Enter()
    {
        _sceneLoader.Load(Initial, EnterLoadLevel);
    }

    public void Exit()
    {
    }

    private void EnterLoadLevel()
    {
        _stateMachine.Enter<LoadProggressState>();
    }

    private void RegisterServices()
    {
        _services.RegisterSingle<IInputService>(InputService());
        _services.RegisterSingle<IAssetProvider>(new AssetProvider());
        _services.RegisterSingle<IPersistentProggressService>(new PersistentProggressService());
        _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>()));
        _services.RegisterSingle<ISaveLoadSerivce>(new SaveLoadService(_services.Single<IPersistentProggressService>(), _services.Single<IGameFactory>()));
    }

    private static IInputService InputService()
    {
        if (Application.isEditor)
            return new StandaloneInputService();
        else
            return new MobileInputService();
    }
}