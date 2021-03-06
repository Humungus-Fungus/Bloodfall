using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State State;

    public void SetState(State state)
    {
        State = state;
        // Debug.Log(State);
        StartCoroutine(State.Start());
    }

    public State ViewState(State state)
    {
        return state;
    }
}
