using Godot;
using System;



[GlobalClass]
public partial class EnemyBeginChase : State
{
    private new Enemy Parent => base.Parent as Enemy;

    private Vector2 _direction;

    public override void Enter(State PreviousState = null)
    {
        Parent.AnimationStateMachine.Travel("anger");

        Parent.AnimationTree.AnimationFinished += OnFinishedAnimation;

        if (PreviousState is MoveState moveState)
        {
            _direction = moveState.Direction;
        }
    }

    private void OnFinishedAnimation(StringName animName)
    {
        if (animName == "anger")
        {
            Parent.StateMachine.ChangeState(StateType.EnemyCharge);
        }
    }

    public override void Exit(State NextState = null)
    {
        if (NextState is MoveState moveState)
        {
            moveState.Direction = _direction;
        }
        Parent.AnimationTree.AnimationFinished -= OnFinishedAnimation;
    }
}
