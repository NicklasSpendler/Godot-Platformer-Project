using Godot;
using System;

[GlobalClass]
public partial class EnemyCharge : MoveState
{
    [Export] private float _chargeTime = 1.5f;
    
    private new Enemy Parent => base.Parent as Enemy;
    public override void Enter(State PreviousState = null)
    {
        Parent.AnimationStateMachine.Travel("run");
        HandleStopCharge();
    }

    public async void HandleStopCharge()
    {
        await ToSignal(Parent.GetTree().CreateTimer(_chargeTime), "timeout");
        if (Parent is Pig parent)
        {
            parent.StateMachine.ChangeState(StateType.EnemyPatrol);
        }
    }
    
    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);
        if (!Parent.IsOnFloor())
        {
            if (Parent is Pig parent)
            {
                parent.StateMachine.ChangeState(StateType.EnemyFall);
            }
        }
    }
    
}
