using Godot;
using System;

[GlobalClass]
public partial class PlayerAttackState : State
{
    [Export] private StateType _nextAttackState;
    [Export] private StringName _attackAnimation;
    
    private new Player Parent => base.Parent as Player;
    
    private bool chainNextAttack = false;

    private Vector2 velocity;
    
    public override void Enter(State PreviousState = null)
    {
        base.Enter(PreviousState);
        
        Parent.AnimationStateMachine.Travel(_attackAnimation);

        Parent.AnimationTree.AnimationFinished += OnAnimationFinished;
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
        Parent.HandleMovement(delta);
    }
}
