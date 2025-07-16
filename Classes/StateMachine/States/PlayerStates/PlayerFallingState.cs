using Godot;
using System;

[GlobalClass]
public partial class PlayerFallingState : State
{
    private new Player Parent => base.Parent as Player;
    
    Vector2 velocity = Vector2.Zero;
    private bool CanCheckFloor = false;
    
    public override void Enter(State PreviousState)
    {
        Parent.AnimationStateMachine.Travel("jump_falling");
        
        velocity = Parent.Velocity;
    }

    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);
        
        if (Parent.IsOnFloor() && Math.Abs(Parent.Velocity.X) > 0)
        {
            Parent.StateMachine.ChangeState(StateType.PlayerRun);
        }
        else if (Parent.IsOnFloor())
        {
            Parent.StateMachine.ChangeState(StateType.PlayerIdle);
        }
        
        HandleFallingMovement(delta);
        
        velocity += Parent.GetGravity() * (float)delta;
        
        Parent.Velocity = velocity;
        
        Parent.MoveAndSlide();
    }
    
    public void HandleFallingMovement(double delta)
    {
        Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        
        if (direction != Vector2.Zero)
        {
            //velocity.X = direction.X * HandleSpeed;
            velocity.X = Mathf.MoveToward(velocity.X, direction.X * Parent.MaxSpeed, Parent.Acceleration * (float)delta);
        }
        else
        {
            velocity.X = Mathf.MoveToward(velocity.X, 0, Parent.Friction * (float)delta);
        }
    }
}
