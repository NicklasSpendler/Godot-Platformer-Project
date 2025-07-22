using Godot;
using System;

[GlobalClass]
public partial class PlayerAttackState : State
{
    [Export] private StringName _attackAnimation;
    [Export] private StateType _nextAttackState;
    
    private new Player Parent => base.Parent as Player;
    
    private bool chainNextAttack = false;

    private Vector2 velocity;
    
    public override void Enter(State PreviousState = null)
    {
        Parent.AnimationStateMachine.Travel(_attackAnimation);

        Parent.HitBox.Monitorable = true;
        Parent.HitBox.Monitoring = true;
        
        Parent.AnimationTree.AnimationFinished += OnAnimationFinished;
    }

    public override void Exit(State NextState = null)
    {
        base.Exit(NextState);
        Parent.HitBox.Monitorable = false;
        Parent.HitBox.Monitoring = false;
    }

    private void OnAnimationFinished(StringName animname)
    {
        if (chainNextAttack)
        {
            StateMachine.ChangeState(_nextAttackState);
        }
        else
        {
            StateMachine.ChangeState(StateType.PlayerIdle);
        }

        chainNextAttack = false;
        Parent.AnimationTree.AnimationFinished -= OnAnimationFinished;
    }

    public override void HandleInput(InputEvent inputEvent)
    {
        base.HandleInput(inputEvent);
        if (Input.IsActionJustPressed("attack"))
        {
            chainNextAttack = true;
        }
    }

    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);
        velocity.X = Mathf.MoveToward(Parent.Velocity.X, 0, Parent.Friction * (float)delta);
        Parent.Velocity = velocity;

        Parent.MoveAndSlide();
    }
}
