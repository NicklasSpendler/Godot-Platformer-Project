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
    Knockback
};

[GlobalClass]
public partial class StateMachine : Resource
{
    [Export] public Godot.Collections.Dictionary<StateType, State> States;
    public State CurrentState;
    
    public Entity Parent;

    public void InitializeStateMachine(Entity parent)
    {
        Parent = parent;
    }

    public State GetState(StateType type)
    {
        return States[type];
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
