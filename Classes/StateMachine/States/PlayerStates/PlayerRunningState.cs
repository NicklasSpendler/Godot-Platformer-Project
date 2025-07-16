using Godot;
using System;

[GlobalClass]
public partial class PlayerRunningState : State
{
    private new Player Parent => base.Parent as Player;
    
    private bool _landing = false;
    
    [Export]
    private Vector2 RightHurtboxPosition { get; set; }
    
    [Export]
    private Vector2 LeftHurtboxPosition { get; set; }
    
    public override void Enter(State PreviousState = null)
    {
        Parent.AnimationStateMachine.Travel("run");
    }

    public override void Exit(State PreviousState = null)
    {
        base.Exit(PreviousState);
    }

    public override void Update(double delta)
    {
        base.Update(delta);

        
        if (Parent.Velocity.X > 0)
        {
            Parent.Sprite2D.FlipH = false;
            Parent.Hurtbox.Position = RightHurtboxPosition;
        }
        else if (Parent.Velocity.X < 0)
        {
            Parent.Sprite2D.FlipH = true;
            Parent.Hurtbox.Position = LeftHurtboxPosition;
        }
    }

    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);
        
        if (Mathf.Abs(Parent.Velocity.X) < 0.1 && Parent.IsOnFloor())
        {
            StateMachine.ChangeState(StateType.PlayerIdle);
        }

        if (!Parent.IsOnFloor())
        {
            StateMachine.ChangeState(StateType.PlayerFall);
        }
        
        Parent.HandleMovementInput(delta);
        Parent.HandleMovement(delta);
        
        if (Input.IsActionJustPressed("jump") && Parent.IsOnFloor())
        {
            Parent.StateMachine.ChangeState(StateType.PlayerJump);
        }
        
        if (_landing)
        {
            if (Parent.AnimationStateMachine.GetCurrentPlayPosition() >=
                Parent.AnimationStateMachine.GetCurrentLength())
            {
                Parent.AnimationStateMachine.Travel("run");
            }
        }
    }
    
    public override void HandleInput(InputEvent @event)
    {
        base.HandleInput(@event);
        if (Input.IsActionJustPressed("attack"))
        {
            StateMachine.ChangeState(StateType.PlayerAttack1);
        }
    }
}
