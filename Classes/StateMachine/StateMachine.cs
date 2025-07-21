using Godot;
using System;

public enum StateType
{
    PlayerIdle,
    PlayerJump,
    PlayerRun,
    PlayerAttack1,
    PlayerAttack2,
    PlayerFall,
    EnemyIdle,
    EnemyFall,
    EnemyPatrol,
    EnemyBeginCharge,
    EnemyCharge,
};

[GlobalClass]
public partial class StateMachine : Resource
{
    [Export] public Godot.Collections.Dictionary<StateType, State> States;
    public State CurrentState;
    
    public CharacterBody2D Parent;

    public void InitializeStateMachine(CharacterBody2D parent)
    {
        Parent = parent;
    }
    
    public void ChangeState(StateType selectedState)
    {
        State newState = States[selectedState];
        if (CurrentState == newState)
        {
            return;
        }

        CurrentState?.Exit(newState);
        
        newState.Parent = Parent;
        newState.StateMachine = this;
        
        newState.Enter(CurrentState);
        CurrentState = newState;
    }
}
