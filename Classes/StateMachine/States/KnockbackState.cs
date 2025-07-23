using Godot;
using System;

[GlobalClass]
public partial class KnockbackState : State
{
    [Export] public string AnimationName;

    private Area2D _damageSource;
    private DamageComponent _damageComponent;

    private Vector2 _velocity = Vector2.Zero;

    private int _knockbackCount;
    
    public override void Enter(State previousState = null)
    {
        if (Parent is Enemy enemy)
        {
            enemy.AnimationStateMachine.Travel("idle");
        }
        else if (Parent is Player player)
        {
            player.AnimationStateMachine.Travel("idle");
        }
        
        Vector2 damageGiverLocation = _damageSource.GetGlobalPosition();
        Vector2 directionToLocation = Parent.GlobalPosition.DirectionTo(damageGiverLocation);
        Vector2 knockBackDirection = new Vector2(-directionToLocation.X, directionToLocation.Y);

        knockBackDirection = new Vector2(knockBackDirection.X * _damageComponent.KnockbackHorizontalStrength, -_damageComponent.KnockbackVerticalStrength);
        
        _velocity = knockBackDirection;
    }

    public override void Exit(State nextState = null)
    {
        base.Exit(nextState);
        _damageSource = null;
        _damageComponent = null;
        _velocity = Vector2.Zero;
        _knockbackCount = 0;
    }

    public void InitializeKnockback(Area2D damageSource, DamageComponent damageComponent)
    {
        _damageSource = damageSource;
        _damageComponent = damageComponent;
    }

    public void RepeatKnockback()
    {
        
        _knockbackCount++;
    }

    public override void PhysicsUpdate(double delta)
    {
        _velocity.X = Mathf.MoveToward(_velocity.X, 0, 100 * (float)delta);
        _velocity += Parent.GetGravity() * (float)delta;
        Parent.Velocity = _velocity;
        Parent.MoveAndSlide();

        if (Parent.IsOnFloor())
        {
            if (Parent is Player)
            {
                Parent.StateMachine.ChangeState(StateType.PlayerIdle);
                
            } else if (Parent is Enemy)
            {
                Parent.StateMachine.ChangeState(StateType.EnemyIdle);
            }
        }
    }
}
