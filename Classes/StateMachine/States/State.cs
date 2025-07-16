using Godot;
using System;

[GlobalClass]
public abstract partial class State : Resource
{
    public CharacterBody2D Parent;
    public StateMachine StateMachine;
    public virtual void Enter(State PreviousState = null)
    {
        
    }

    public virtual void Exit(State NextState = null)
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
