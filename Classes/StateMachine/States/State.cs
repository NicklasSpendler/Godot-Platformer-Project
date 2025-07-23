using Godot;
using System;

[GlobalClass]
public abstract partial class State : Resource
{
    public Entity Parent;
    public StateMachine StateMachine;
    public abstract void Enter(State previousState = null);

    public virtual void Exit(State nextState = null)
    {
    }
    
    public virtual void Update(double delta)
    {
        
    }
    
    public virtual void PhysicsUpdate(double delta)
    {
        
    }

    public virtual void HandleInput(InputEvent inputEvent)
    {
        
    }
}
