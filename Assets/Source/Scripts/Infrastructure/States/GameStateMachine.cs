using System;
using System.Collections.Generic;

public class GameStateMachine
{
    private readonly Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;

    public GameStateMachine(SceneLoader sceneLoader, LoadingScreen loadingScreen, AllServices services)
    {
        _states = new Dictionary<Type, IExitableState>
        {
            [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
            [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingScreen, services.Single<IGameFactory>(), services.Single<IPersistentProggressService>()),
            [typeof(LoadProggressState)] = new LoadProggressState(this, services.Single<IPersistentProggressService>(), services.Single<ISaveLoadSerivce>()),
            [typeof(GameLoopState)] = new GameLoopState(this),
        };
    }

    public void Enter<TState>() where TState : class, IState
    {
        IState state = ChangedState<TState>();
        state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
        TState state = ChangedState<TState>();
        state.Enter(payload);
    }

    private TState ChangedState<TState>() where TState : class, IExitableState
    {
        _activeState?.Exit();

        TState state = GetState<TState>();
        _activeState = state;

        return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState
    {
        return _states[typeof(TState)] as TState;
    }
}