using Godot;
using System;

[GlobalClass]
public partial class EnemyIdle : State
{
    private new Enemy Parent => base.Parent as Enemy;

    public override void Enter(State previousState = null)
    {
        Parent.AnimationStateMachine.Travel("idle");
    }

    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);
        
        if (Parent.IsOnFloor())
        {
            Parent.StateMachine.ChangeState(StateType.EnemyPatrol);
        }
        else
        {
            GD.Print("Fall");
        }
    }
}