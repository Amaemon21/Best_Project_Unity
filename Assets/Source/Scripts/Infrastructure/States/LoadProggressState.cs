internal class LoadProggressState : IState
{
    private readonly GameStateMachine _stateMachine;
    private readonly IPersistentProggressService _proggressService;
    private readonly ISaveLoadSerivce _saveLoadService;

    public LoadProggressState(GameStateMachine stateMachine, IPersistentProggressService proggressService, ISaveLoadSerivce saveLoadService)
    {
        _stateMachine = stateMachine;
        _proggressService = proggressService;
        _saveLoadService = saveLoadService;
    }

    public void Enter()
    {
        LoadProggressOrInitNew();

        _stateMachine.Enter<LoadLevelState, string>(_proggressService.PlayerProgress.WorldData.PositionOnLevel.Level);
    }

    public void Exit()
    {
    }

    private void LoadProggressOrInitNew()
    {
        _proggressService.PlayerProgress = _saveLoadService.LoadProggress() ?? NewProggress();
    }

    private PlayerProgress NewProggress()
    {
        PlayerProgress playerProgress  = new PlayerProgress("Gameplay");

        playerProgress.HealthStats.MaxHealth = 50;
        playerProgress.HealthStats.ResetHealth();

        playerProgress.DamageStats.Damage = 100;
        playerProgress.DamageStats.Radius = 0.5f;


        return playerProgress;
    }
}
