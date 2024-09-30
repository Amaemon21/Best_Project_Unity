using UnityEngine;

public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
{
    [SerializeField] private LoadingScreen _loadingScreen;

    private Game _game;

    private void Awake()
    {
        LoadingScreen loadingScreen = Instantiate(_loadingScreen, transform);

        _game = new Game(this, loadingScreen);
        _game.StateMachine.Enter<BootstrapState>();
         
        DontDestroyOnLoad(this);
    }
}