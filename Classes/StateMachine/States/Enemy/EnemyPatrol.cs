using Godot;
using System;

public enum DirectionEnum
{
    Left,
    Right,
}

[GlobalClass]
public partial class EnemyPatrol : MoveState
{
    //new is required because it hides the original Parent variable
    private new Enemy Parent => base.Parent as Enemy;
    
    [Export]
    public Vector2 WallDetectionRightPos { get; set; }
    [Export]
    public Vector2 FloorDetectionRightPos { get; set; }
    
    [Export]
    public Vector2 WallDetectionLeftPos { get; set; }
    [Export]
    public Vector2 FloorDetectionLeftPos { get; set; }
    
    [Export]
    public Vector2 ExpressionRightPos { get; set; }
    [Export]
    public Vector2 ExpressionLeftPos { get; set; }
    
    [Export]
    public float WallDetectionLength { get; set; }

    [Export] private DirectionEnum InitialDirection;
    
    public override void Enter(State PreviousState = null)
    {
        if (Parent is Pig parent)
        {
            parent.VisionShapeCast.Enabled = true;
        }
        
        Parent.AnimationStateMachine.Travel("walk");

        if (InitialDirection == DirectionEnum.Left)
        {
            Direction = Vector2.Left;
        }else if (InitialDirection == DirectionEnum.Right)
        {
            Direction = Vector2.Right;
        }
    }
    
    public override void PhysicsUpdate(double delta)
    {
        if (!Parent.IsOnFloor())
        {
            Parent.StateMachine.ChangeState(StateType.EnemyFall);
        }
        
        base.PhysicsUpdate(delta);
        
        if (Parent is Pig parent)
        {
            if (parent.WallDetection.IsColliding())
            {
                ChangeDirection();
            }

            if (parent.IsOnFloor() && !parent.FloorDetection.IsColliding())
            {
                ChangeDirection();
            }
            
            
            if (parent.VisionShapeCast.IsColliding())
            {
                parent.StateMachine.ChangeState(StateType.EnemyBeginCharge);
            }
        }
        
        if (Direction.X >= 0.1)
        {
            Parent.Sprite2D.FlipH = false;
        }
        else if (Direction.X <= -0.1)
        {
            Parent.Sprite2D.FlipH = true;
        }
    }

    private void ChangeDirection()
    {
        Direction.X *= -1;
        if (Parent is Pig parent)
        {
            Vector2 currentwallDetectionPosition = parent.WallDetection.TargetPosition;
            Vector2 newWallDetectionPosition = new Vector2(currentwallDetectionPosition.X * -1, 0);
            parent.WallDetection.TargetPosition = newWallDetectionPosition;

            parent.VisionShapeCast.TargetPosition = new Vector2(parent.VisionShapeCast.TargetPosition.X * -1,0);
            
            if (Direction.X >= 0.1)
            {
                parent.WallDetection.Position = WallDetectionRightPos;
                parent.FloorDetection.Position = FloorDetectionRightPos;
                parent.ExpressionSprite.Position = ExpressionRightPos;
            }
            else if (Direction.X <= -0.1)
            {
                parent.WallDetection.Position = WallDetectionLeftPos;
                parent.FloorDetection.Position = FloorDetectionLeftPos;
                parent.ExpressionSprite.Position = ExpressionLeftPos;
            }    
        }
    }
}
