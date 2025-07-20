using Godot;
using System;
[GlobalClass]
public partial class PlayerJumpingState : State
{
    private new Player Parent => base.Parent as Player;
    
    Vector2 velocity = Vector2.Zero;
    private bool CanCheckFloor = false;
    
    public override async void Enter(State PreviousState)
    {
        CanCheckFloor = false;
        
        velocity = Parent.Velocity;

        Parent.AnimationStateMachine.Start("jump");
        velocity.Y = Parent.JumpVelocity;
        
        await ToSignal(Parent.GetTree().CreateTimer(0.1f), "timeout");
        CanCheckFloor = true;
    }

    public  override void PhysicsUpdate(double delta)
    { 
        base.PhysicsUpdate(delta);
        
        if (Parent.IsOnFloor() && CanCheckFloor)
        {
            Parent.StateMachine.ChangeState(StateType.PlayerIdle);
        }

        if (Parent.Velocity.Y > 0)
        {
            Parent.StateMachine.ChangeState(StateType.PlayerFall);
        }
        
        HandleJumpingMovement(delta);
        
        velocity += Parent.GetGravity() * (float)delta;
        
        Parent.Velocity = velocity;
        
        Parent.MoveAndSlide();
    }

    public void HandleJumpingMovement(double delta)
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
