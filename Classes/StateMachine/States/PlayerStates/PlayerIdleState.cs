using Godot;
using System;

[GlobalClass]
public partial class PlayerIdleState : MoveState
{
    private new Player Parent => base.Parent as Player;
    
    private bool _landing = false;
    
    public override void Enter(State PreviousState = null)
    {
        Parent.AnimationStateMachine.Travel("idle");
    }

    public override void Exit(State PreviousState = null)
    {
        base.Exit(PreviousState);
    }
    
    public override void Update(double delta)
    {

    }

    public override void PhysicsUpdate(double delta)
    {
        if (Mathf.Abs(Parent.Velocity.X) > 0.1 && Parent.IsOnFloor())
        {
            StateMachine.ChangeState(StateType.PlayerRun);
        }
        
        Direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        
        base.PhysicsUpdate(delta);
        

        if (Input.IsActionJustPressed("jump") && Parent.IsOnFloor())
        {
            StateMachine.ChangeState(StateType.PlayerJump);
        }
        
        if (_landing)
        {
            if (Parent.AnimationStateMachine.GetCurrentPlayPosition() >=
                Parent.AnimationStateMachine.GetCurrentLength())
            {
                Parent.AnimationStateMachine.Travel("idle");
            }
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
