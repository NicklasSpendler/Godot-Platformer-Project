using Godot;
using System;

public partial class MoveState : State
{
    [Export]
    public float MaxSpeed = 300.0f;
    [Export]
    public float Acceleration = 800.0f;
    [Export]
    public float JumpVelocity = -400.0f;
    [Export]
    public float Friction = 1000.0f;
    
    public Vector2 Direction = Vector2.Zero;
    
    public override void Enter(State PreviousState = null)
    {
        throw new NotImplementedException();
    }

    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);
        
        
        Vector2 velocity = Parent.Velocity;
        if (Direction != Vector2.Zero)
        {
            velocity.X = Mathf.MoveToward(Parent.Velocity.X, Direction.X * MaxSpeed, Acceleration * (float)delta);
        }
        else
        {
            velocity.X = Mathf.MoveToward(Parent.Velocity.X, 0, Friction * (float)delta);
        }
        
        Parent.Velocity = velocity;
        
        Parent.MoveAndSlide();
    }
}
