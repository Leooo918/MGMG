using UnityEngine;

public abstract class State<T>
{
    public T Owner { get; private set; }
    public StateMachine<T> StateMachine { get; private set; }

    public State(T owner, StateMachine<T> stateMachine)
    {
        Owner = owner;
        StateMachine = stateMachine;
    }

    public virtual void OnEnterState()
    {

    }

    public virtual void OnExitState()
    {

    }

    public virtual void OnUpdateState()
    {

    }
}
