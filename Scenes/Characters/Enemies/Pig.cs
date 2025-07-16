using Godot;
using System;

public partial class Pig : Enemy
{
    [Export] public RayCast2D WallDetection;
    [Export] public RayCast2D FloorDetection;
    public override void _Ready()
    {
        base._Ready();

        if (IsOnFloor())
        {
            StateMachine.ChangeState(StateType.EnemyIdle);
        }
        else
        {
            StateMachine.ChangeState(StateType.EnemyFall);
        }
    }
}
