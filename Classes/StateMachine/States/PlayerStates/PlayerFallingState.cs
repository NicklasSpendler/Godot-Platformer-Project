using Godot;
using System;

[GlobalClass]
public partial class PlayerFallingState : State
{
    private new Player Parent => base.Parent as Player;
    
    private Vector2 _velocity = Vector2.Zero;
    private bool CanCheckFloor = false;
    
    public override void Enter(State previousState)
    {
        Parent.AnimationStateMachine.Travel("jump_falling");
        
        _velocity = Parent.Velocity;
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
        
        _velocity += Parent.GetGravity() * (float)delta;
        
        Parent.Velocity = _velocity;
        
        Parent.MoveAndSlide();
    }
    
    public void HandleFallingMovement(double delta)
    {
        Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        
        if (direction != Vector2.Zero)
        {
            //velocity.X = direction.X * HandleSpeed;
            _velocity.X = Mathf.MoveToward(_velocity.X, direction.X * Parent.MaxSpeed, Parent.Acceleration * (float)delta);
        }
        else
        {
            _velocity.X = Mathf.MoveToward(_velocity.X, 0, Parent.Friction * (float)delta);
        }
    }
}
