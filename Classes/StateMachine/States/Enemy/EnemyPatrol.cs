using Godot;
using System;

[GlobalClass]
public partial class EnemyPatrol : State
{
    //new is required because it hides the original Parent variable
    private new Enemy Parent => base.Parent as Enemy;
    
    //1 = Right | -1 = Left
    private int _direction = 1;
    
    [Export]
    public Vector2 WallDetectionRightPos { get; set; }
    [Export]
    public Vector2 FloorDetectionRightPos { get; set; }
    
    [Export]
    public Vector2 WallDetectionLeftPos { get; set; }
    [Export]
    public Vector2 FloorDetectionLeftPos { get; set; }
    
    [Export]
    public float WallDetectionLength { get; set; }
    
    public override void Enter(State PreviousState = null)
    {
        base.Enter(PreviousState);
        if (Parent is Pig parent)
        {
            
        }
        
        Parent.AnimationStateMachine.Travel("walk");
    }
    
    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);
        if (!Parent.IsOnFloor())
        {
            Parent.StateMachine.ChangeState(StateType.EnemyFall);
        }

        if (_direction >= 1)
        {
            Parent.Sprite2D.FlipH = false;
        }
        else
        {
            Parent.Sprite2D.FlipH = true;
        }
        
        Parent.Velocity = new Vector2(100*_direction, 0);
        
        Parent.MoveAndSlide();
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
        }
    }

    private void ChangeDirection()
    {
        _direction *= -1;
        if (Parent is Pig parent)
        {
            Vector2 currentwallDetectionPosition = parent.WallDetection.TargetPosition;
            Vector2 newWallDetectionPosition = new Vector2(currentwallDetectionPosition.X * -1, 0);
            parent.WallDetection.TargetPosition = newWallDetectionPosition;
            
            if (_direction == 1)
            {
                parent.WallDetection.Position = WallDetectionRightPos;
                parent.FloorDetection.Position = FloorDetectionRightPos;
            }
            else if (_direction == -1)
            {
                parent.WallDetection.Position = WallDetectionLeftPos;
                parent.FloorDetection.Position = FloorDetectionLeftPos;
            }    
        }
    }
}
