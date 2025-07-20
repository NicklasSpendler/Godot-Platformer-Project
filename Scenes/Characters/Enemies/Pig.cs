using Godot;
using System;

public partial class Pig : Enemy
{
    [Export] public RayCast2D WallDetection;
    [Export] public RayCast2D FloorDetection;
    
    [Export] public ShapeCast2D VisionShapeCast;

    [Export] public Sprite2D ExpressionSprite;
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
