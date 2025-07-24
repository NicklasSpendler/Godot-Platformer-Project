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
    
    private bool _isSignalConected = false;
    
    public override void Enter(State previousState = null)
    {
        if (_isSignalConected)
        {
            Parent.AnimationTree.AnimationFinished -= OnAnimationFinished;
            _isSignalConected = false;
        }
        Parent.AnimationStateMachine.Travel(_attackAnimation);
        
        Parent.HitBox.SetDeferred("monitorable", true);
        Parent.HitBox.SetDeferred("monitoring", true);
        
        Parent.AnimationTree.AnimationFinished += OnAnimationFinished;
        _isSignalConected = true;
    }

    public override void Exit(State nextState = null)
    {
        base.Exit(nextState);
        Parent.HitBox.SetDeferred("monitorable", false);
        Parent.HitBox.SetDeferred("monitoring", false);
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
        _isSignalConected = false;
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
