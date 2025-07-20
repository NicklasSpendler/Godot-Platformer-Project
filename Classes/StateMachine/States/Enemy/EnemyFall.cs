using Godot;
using System;

[GlobalClass]
public partial class EnemyFall : State
{
    private new Enemy Parent => base.Parent as Enemy;

    private Vector2 velocity;
    
    public override void Enter(State PreviousState = null)
    {
        Parent.AnimationStateMachine.Travel("idle");

        velocity = Parent.Velocity;
    }

    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);

        if (Parent.IsOnFloor())
        {
            StateMachine.ChangeState(StateType.EnemyIdle);
        }
        
        velocity += Parent.GetGravity() * (float)delta;
        Parent.Velocity = velocity;

        Parent.MoveAndSlide();
    }
}
