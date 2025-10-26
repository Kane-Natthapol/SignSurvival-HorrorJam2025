using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState, BaseState<EState>> States =  new Dictionary<EState,BaseState<EState>>(); 
    protected BaseState<EState> CurrentState;
    protected bool isTransitioningToState;

    public virtual void Start()
    {
        CurrentState.EnterState();
    }

    public virtual void Update()
    {
        EState nextStateKey = CurrentState.GetNextState();
        if (!isTransitioningToState && nextStateKey.Equals(CurrentState.StateKey))
        {
            CurrentState.UpdateState();
        }
        else if(!isTransitioningToState)
        {
            TransitionToState(nextStateKey);
        }
    }
    public void TransitionToState(EState stateKey)
    {
        isTransitioningToState = true;
        CurrentState.ExitState();
        CurrentState = States[stateKey];
        CurrentState.EnterState();
        isTransitioningToState = false;

    }
    private void OnTriggerEnter(Collider other)
    {
        CurrentState.OnTriggerEnter(other);
    }
    private void OnTriggerStay(Collider other)
    {
        CurrentState.OnTriggerStay(other);
    }
    private void OnTriggerExit(Collider other)
    {
        CurrentState.OnTriggerExit(other);
    }
}