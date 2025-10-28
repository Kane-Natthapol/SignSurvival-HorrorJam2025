using UnityEngine;

public abstract class GameState : BaseState<GameStateMachine.EState>
{
    protected GameStateContext Context;

    public GameState(GameStateContext context, GameStateMachine.EState stateKey) : base(stateKey)
    {
        Context = context;
    }
}
