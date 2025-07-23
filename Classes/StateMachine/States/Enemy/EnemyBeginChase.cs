using Godot;
using System;



[GlobalClass]
public partial class EnemyBeginChase : State
{
    private new Enemy Parent => base.Parent as Enemy;

    private Vector2 _direction;

    public override void Enter(State previousState = null)
    {
        Parent.AnimationStateMachine.Travel("anger");

        Parent.AnimationTree.AnimationFinished += OnFinishedAnimation;

        if (previousState is MoveState moveState)
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

    public override void Exit(State nextState = null)
    {
        if (nextState is MoveState moveState)
        {
            moveState.Direction = _direction;
        }
        Parent.AnimationTree.AnimationFinished -= OnFinishedAnimation;
    }
}
