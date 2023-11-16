using System;
using System.Collections.Generic;
using _Scripts.Infrastructure.Audio;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Factory.UIFactory;
using _Scripts.Infrastructure.Services.PersistentProgress;
using _Scripts.Infrastructure.Services.SaveLoad;
using _Scripts.Infrastructure.States;
using _Scripts.StaticData;

namespace _Scripts.Infrastructure.Services
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _state;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain, AllServices services)
        {
            _state = new()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadProgressState)] = new LoadProgressState(this,
                    services.Single<IPersistentProgressService>(), 
                    services.Single<ISaveLoadService>()),
                [typeof(LoadSceneState)] = new LoadSceneState(
                    this, sceneLoader, curtain, 
                    services.Single<IUIFactory>(), 
                    services.Single<IStaticDataService>(),
                    services.Single<IGameFactory>(), 
                    services.Single<IPersistentProgressService>(), 
                    services.Single<IAudioService>()),
                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit(); //в первом стайте null будет поэтому ?

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _state[typeof(TState)] as TState;
    }
}