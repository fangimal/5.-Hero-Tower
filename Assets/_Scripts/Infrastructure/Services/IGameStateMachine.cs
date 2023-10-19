using _Scripts.Infrastructure.States;

namespace _Scripts.Infrastructure.Services
{
    public interface IGameStateMachine: IService
    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
    }
}