using System.Collections.Generic;

public class StateMachine<T>
{
    public T Owner { get; private set; }
    private State<T> _currentState;
    private List<State<T>> _stateList;
    public StateMachine(T owner)
    {

    }

    public void ChangeState()
    {

    }
}
