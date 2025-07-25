using Godot;
using System;

[GlobalClass]
public partial class PlayerRunningState : MoveState
{
    private new Player Parent => base.Parent as Player;
    
    private bool _landing = false;
    
    [Export]
    private Vector2 RightHitboxPosition { get; set; }
    
    [Export]
    private Vector2 LeftHitboxPosition { get; set; }
    
    public override void Enter(State previousState = null)
    {
        Parent.AnimationStateMachine.Travel("run");
    }

    public override void Exit(State nextState = null)
    {
        base.Exit(nextState);
    }

    public override void Update(double delta)
    {
        base.Update(delta);
        
        if (Parent.Velocity.X > 0)
        {
            Parent.Sprite2D.FlipH = false;
            Parent.HitBox.Position = RightHitboxPosition;
        }
        else if (Parent.Velocity.X < 0)
        {
            Parent.Sprite2D.FlipH = true;
            Parent.HitBox.Position = LeftHitboxPosition;
        }
    }

    public override void PhysicsUpdate(double delta)
    {
        
        Direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        
        base.PhysicsUpdate(delta);
        
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

        if (Parent.Velocity.Length() <= 0)
        {
            Parent.StateMachine.ChangeState(StateType.PlayerIdle);
        }
        
        if (!Parent.IsOnFloor())
        {
            Parent.StateMachine.ChangeState(StateType.PlayerFall);
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
